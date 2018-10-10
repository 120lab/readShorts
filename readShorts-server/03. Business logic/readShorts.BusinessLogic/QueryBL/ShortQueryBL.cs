using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using readShorts.BusinessLogic.ServiceBL;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic
{
    public class ShortQueryBL : BaseBL, IShortQueryBL
    {
        private readonly IShortQueryRepository _shortRepository;
        private readonly ILookupQueryRepository _lookupRepository;
        private readonly IUserAccountQueryRepository _userAccountQueryRepository;

        public ShortQueryBL(IShortQueryRepository shortRepository, IUnitOfWork unitOfWork, ILoggerService loggerService, 
            ILookupQueryRepository lookupRepository, IUserAccountQueryRepository userAccountQueryRepository)
            : base(unitOfWork, loggerService)
        {
            _shortRepository = shortRepository;
            _lookupRepository = lookupRepository;
            _userAccountQueryRepository = userAccountQueryRepository;
        }

        public ShortViewModel GetShorts(ShortQuery query)
        {
            ShortViewModel shortVM = new ShortViewModel();
            IEnumerable<Entities.dbo.Short> result = null;

            if (query != null && query.RecordKey != 0)
            {
                result = _shortRepository.GetShort(query.RecordKey);
            }
            else if (query != null && query.LUShortAgeRestrictionKey != 0)
            {
                result = _shortRepository.GetShortsByGetByAgeRestriction(query.LUShortAgeRestrictionKey);
            }
            else
            {
                result = _shortRepository.GetShorts();
            }

            if (query.EnrichData)
            {
                IQueryable<Entities.LOOKUP.LUShortAgeRestriction> ages = _lookupRepository.GetLUShortAgeRestrictions(0);
                IQueryable<Entities.LOOKUP.LUSysInterfaceLanguage> lngs = _lookupRepository.GetLUSysInterfaceLanguages(0);
                IQueryable<Entities.LOOKUP.LUQuoteType> quots = _lookupRepository.GetLUQuoteType(0);
                IQueryable<Entities.LOOKUP.LUStoryType> stors = _lookupRepository.GetLUStoryType(0);
                IQueryable<Entities.LOOKUP.LUShortCategoryType> catgs = _lookupRepository.GetLUShortCategoryTypes(0);
                IQueryable<Entities.dbo.UserAccount> usrs = _userAccountQueryRepository.GetAll();

                shortVM.Shorts = from sua in result
                                 join age in ages
                                            on sua.LUShortAgeRestrictionKey equals age.RecordKey
                                 join lang in lngs
                                            on sua.LUSysInterfacelanguageKey equals lang.RecordKey
                                 join quot in quots
                                            on sua.LUQuoteTypeKey equals quot.RecordKey
                                 join stor in stors
                                            on sua.LUStoryTypeKey equals stor.RecordKey
                                 join catg in catgs
                                            on sua.LUCategoryTypeKey equals catg.RecordKey
                                 join wrtr in usrs
                                            on sua.WriterUserKey equals wrtr.RecordKey
                                 where sua.IsRowDeleted == false
                                 select new Models.dbo.Short
                                 {
                                     RecordKey = sua.RecordKey,
                                     Title = sua.Title,
                                     CategoryPicturePath = sua.CategoryPicturePath,
                                     VideoPath = sua.VideoPath,
                                     Embed = sua.Embed,
                                     Quote = sua.Quote,
                                     Text = sua.Text,
                                     ERTInMiliSeconds = sua.ERTInMiliSeconds,
                                     IsPublic = sua.IsPublic,
                                     SharePicturePath = sua.SharePicturePath,
                                     LUQuoteTypeKey = sua.LUQuoteTypeKey,
                                     LUQuoteTypeName = quot.Description,
                                     LUStoryTypeKey = sua.LUStoryTypeKey,
                                     LUStoryTypeName = stor.Description,
                                     LUCategoryTypeKey = sua.LUCategoryTypeKey,
                                     LUCategoryTypeName = catg.Description,
                                     BackgroundPicturePath = sua.BackgroundPicturePath,
                                     LUShortAgeRestrictionKey = sua.LUShortAgeRestrictionKey,
                                     LUShortAgeRestrictionName = age.Description,
                                     LUSysInterfacelanguageKey = sua.LUSysInterfacelanguageKey,
                                     LUSysInterfacelanguageName = lang.Description,
                                     WriterUserKey = sua.WriterUserKey,
                                     WriterUserName = wrtr.UserName,
                                     WriterFirstName = wrtr.FirstName,
                                     WriterLastName = wrtr.LastName,
                                     JsonData = sua.JsonData,
                                     UniqueGuid = sua.UniqueGuid                                     
                                 };

            }
            else
            {
                shortVM.Shorts = base.Map<IEnumerable<Entities.dbo.Short>, IEnumerable<Models.dbo.Short>>(result);
            }
            return shortVM;
        }

        public IDictionary<long, string> GetShortsJsonData()
        {
            string cacheKey = string.Format("Class_{0}_Method_{1}_Param_{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, "");

            var cacheData = CacheHandler.Instance.GetFromCache(cacheKey);

            if (cacheData != null)
            {
                IDictionary<long, string> cached = JsonConvert.DeserializeObject<IDictionary<long, string>>(cacheData.ToString());
                return cached;
            }
            else
            {
                IDictionary<long, string> data = new Dictionary<long, string>();
                Parallel.ForEach(_shortRepository.GetShorts(), (entity) =>
                {
                    lock (thisLock)
                    {
                        data.Add(entity.RecordKey, entity.JsonData);
                    }
                }
                );
                CacheHandler.Instance.AddToCache(cacheKey, JObject.FromObject(data), CacheHandler.ObjectPolicy.RefreshedTables);

                return data;
            }
        }
    }
}