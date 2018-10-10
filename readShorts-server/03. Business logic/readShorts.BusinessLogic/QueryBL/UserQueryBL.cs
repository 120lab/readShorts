using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.DataAccess.Interfaces;
using readShorts.Entities.dbo;
using readShorts.Models.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic
{
    public class UserQueryBL : BaseBL, IUserQueryBL
    {
        public const string CONST_USER_NOT_EXIST = "User not exist";
        private readonly IUserAccountQueryRepository _userRepository;
        private readonly ILookupQueryBL _lookupQueryBL;
        private readonly IEventUserAccountQueryBL _euaqBL;

        public UserQueryBL(IUserAccountQueryRepository userQueryRepository,
            IUnitOfWork unitOfWork, ILoggerService loggerService, ILookupQueryBL lookupQuery, IEventUserAccountQueryBL euaqBL)
            : base(unitOfWork, loggerService)
        {
            _userRepository = userQueryRepository;
            _lookupQueryBL = lookupQuery;
            _euaqBL = euaqBL;
        }

        public Int64? GetUser(string userName)
        {
            Int64? user = null;

            string cacheKey = string.Format("Class_{0}_Method_{1}_Param_{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, userName);

            var cacheData = CacheHandler.Instance.GetFromCache(cacheKey);

            if (cacheData != null)
            {
                user = JsonConvert.DeserializeObject<UserAccount>(cacheData.ToString()).RecordKey;
            }
            else
            {
                UserAccount result = _userRepository.GetEveryUserByUserName(userName);

                if (result != null)
                {
                    user = result.RecordKey;
                    CacheHandler.Instance.AddToCache(cacheKey, JObject.FromObject(result), CacheHandler.ObjectPolicy.RefreshedTables);
                }

            }

            return user;
        }

        public UserViewModel GetUser(UserQuery query)
        {
            Entities.dbo.UserAccount result = null;     

            if (!string.IsNullOrEmpty(query.UserName))
            {
                result = _userRepository.GetEveryUserByUserName(query.UserName);
            }
            else if (!string.IsNullOrEmpty(query.Identity) && !string.IsNullOrEmpty(query.Password))
            {
                result =  _userRepository.GetLoginVerifiedUser(query.Identity, query.Password);
            }
            else if (!string.IsNullOrEmpty(query.Identity))
            {
                result =  _userRepository.GetLoginVerifiedUserIdentityOnly(query.Identity);
            }
            else if (!string.IsNullOrEmpty(query.Guid))
            {
                result =  _userRepository.GetEveryUserByGuid(query.Guid);
            }

            UserViewModel userVM = new UserViewModel();

            if (result == null)
            {
                List<Models.Message> msgs = new List<Models.Message>();
                msgs.Add(new Models.Message(Models.LogLevel.Error, CONST_USER_NOT_EXIST));
                userVM.Messages = msgs;
            }
            else
            {
                Models.dbo.UserAccount ua = base.Map<readShorts.Entities.dbo.UserAccount, readShorts.Models.dbo.UserAccount>(result);
                if (query.EnrichData)
                {
                    EnrichUserData(ua);
                }
                userVM.Users = new List<Models.dbo.UserAccount> { ua };
            }
            return userVM; 
        }

        private void EnrichUserData(Models.dbo.UserAccount result)
        {
            var eventItem = _euaqBL.GetEventUserAccount(new EventUserAccountQuery() { UserAccoutKey = result.RecordKey });

            Models.dbo.EventUserAccount eua = eventItem.UserEvents.FirstOrDefault(x => x.LUEventTypeKey == (long)Models.Enums.LUEventType.UserInProfilePage);
            if (eua != null)
            {
                result.UserInProfilePage = true;
            }

            result.StrBirthDate = result.BirthDate.ToString("dd/MM/yyyy");
            IEnumerable<Models.LOOKUP.LookupBase> s = _lookupQueryBL.Get("LUSubscriptionTypes", result.LUSysInterfacelanguageKey).Lookups;
            if (s != null)
            {
                result.LUSubscriptionTypeName = s.Where(x => x.RecordKey == result.LUSubscriptionTypeKey).FirstOrDefault().Description;
            }

            IEnumerable<Models.LOOKUP.LookupBase> l = _lookupQueryBL.Get("LUSysInterfaceLanguages", result.LUSysInterfacelanguageKey).Lookups;
            if (l != null)
            {
                result.LUSysInterfacelanguageName = l.Where(x => x.RecordKey == result.LUSysInterfacelanguageKey).FirstOrDefault().Description;
            }

            IEnumerable<Models.LOOKUP.LookupBase> g = _lookupQueryBL.Get("LUGenders", result.LUSysInterfacelanguageKey).Lookups;
            if (g != null)
            {
                result.LUGenderName = g.Where(x => x.RecordKey == result.LUGenderKey).FirstOrDefault().Description;
            }

            IEnumerable<Models.LOOKUP.LookupBase> c = _lookupQueryBL.Get("LUCountries", result.LUSysInterfacelanguageKey).Lookups;
            if (c != null)
            {
                result.LUCountryName = c.Where(x => x.RecordKey == result.LUCountryKey).FirstOrDefault().Description;
            }
        }

        public UserViewModel GetUsers()
        {
            IEnumerable<Entities.dbo.UserAccount> entities = _userRepository.GetAllUserAccounts();

            UserViewModel userVM = new UserViewModel();
            userVM.Users = base.Map<IEnumerable<readShorts.Entities.dbo.UserAccount>, IEnumerable<readShorts.Models.dbo.UserAccount>>(entities);

            //foreach (Models.dbo.UserAccount usr in userVM.Users)
            //{
            //    EnrichUserData(usr);
            //}

            return userVM;
        }
    }
}