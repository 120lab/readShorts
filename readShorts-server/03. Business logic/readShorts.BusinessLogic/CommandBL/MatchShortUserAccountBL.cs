using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Newtonsoft.Json;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models;
using readShorts.Models.Commands;
using readShorts.Models.dbo;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic
{
    public class MatchShortUserAccountBL : BaseBL, IMatchShortUserAccountBL
    {
        private const int CONST_NUMBER_OF_SHORTS_PER_AD = 5;
        private const int CONST_MAX_UNREAD_NUMBER_OF_SHORTS_PER_USER_ACCOUNT = 100;
        private const string CONST_PARAM_OBJECT_IS_NULL = "Param object is null";
        private const string CONST_VALUE_OUT_OF_RANGE = "{0} value is {1}, out of range";
        private const string CONST_VALUE_IS_EMPTY = "{ 0 } value is {1}, is empty";

        private readonly IUserQueryBL _userQueryBL;
        private readonly IShortQueryBL _shortQueryBL;
        private readonly IShortUserAccountQueryBL _shortUserAccountQueryBL;
        private readonly IShortUserAccountCommandBL _shortUserAccountCommandBL;
        private readonly IAdQueryBL _AdQueryBL;
        private readonly ILookupQueryBL _LookupQueryBL;
        private readonly IMatchAlgoBL _MatchAlgo;

        public MatchShortUserAccountBL(IUserQueryBL userQueryBL, IShortQueryBL shortQueryBL, IAdQueryBL adQueryBL,
             IShortUserAccountCommandBL shortUserAccountCommandBL, IShortUserAccountQueryBL shortUserAccountQueryBL,
            IUnitOfWork unitOfWork, ILoggerService loggerService, ILookupQueryBL lookupQueryBL, IMatchAlgoBL matchAlgoBL)
            : base(unitOfWork, loggerService)
        {
            _shortQueryBL = shortQueryBL;
            _userQueryBL = userQueryBL;
            _shortUserAccountQueryBL = shortUserAccountQueryBL;
            _shortUserAccountCommandBL = shortUserAccountCommandBL;
            _AdQueryBL = adQueryBL;
            _LookupQueryBL = lookupQueryBL;
            _MatchAlgo = matchAlgoBL;
        }

        public MessageMatchShortUserAccountViewModel MatchData(MatchShortUserAccountCommand comm)
        {
            MessageMatchShortUserAccountViewModel shortVM = new MessageMatchShortUserAccountViewModel();

            /// Get User
            Int64? user = _userQueryBL.GetUser(comm.UserName);

            /// Validate params
            shortVM.Messages = ValidateParam(comm, user);

            if (shortVM.Messages == null || (shortVM.Messages != null && shortVM.Messages.Count() == 0))
            {
                shortVM.Matches = GetMatchData(comm, user.Value);

                if (shortVM.Matches.Count() > 0)
                {
                    shortVM.NetworkData = GetMatchPLM();
                }
                shortVM.PLMData = new Random().Next(1, 1000);

            }
            return shortVM;
        }

        private IEnumerable<MessageMatchShortUserAccountViewModel.UserMatchNetworkData> GetMatchPLM()
        {
            /// TBD
            List<MessageMatchShortUserAccountViewModel.UserMatchNetworkData> net = new List<MessageMatchShortUserAccountViewModel.UserMatchNetworkData>();
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "80" });
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "70" });
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "30" });
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "25" });
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "87" });
            net.Add(new MessageMatchShortUserAccountViewModel.UserMatchNetworkData() { PicturePath = @"https://cdn-images-1.medium.com/fit/c/100/100/0*6eG1RCaX6199ZV5e.jpg", MatchPercentage = "46" });

            return net;
        }

        //private IEnumerable<Message> UpdateSignShortSendToUser(IEnumerable<long> shortUserAccounts, UserAccount user)
        //{
        //    ShortUserAccountViewModel suavm = new ShortUserAccountViewModel();
        //    foreach (long item in shortUserAccounts)
        //    {
        //        suavm = _shortUserAccountCommandBL.UpdateShortUserAccount(new UpdateShortUserAccountCommand()
        //        {
        //            UserAccountKey = user.RecordKey,
        //            ShortKey = item,
        //            ShortSendToUser = true
        //        });
        //    }

        //    return suavm.Messages;
        //}

        private IEnumerable<Models.Message> ValidateParam(MatchShortUserAccountCommand comm, Int64? users)
        {
            List<Models.Message> msgs = new List<Message>();

            if (comm == null)
            {
                msgs.Add(new Models.Message(LogLevel.Error, string.Format(CONST_PARAM_OBJECT_IS_NULL)));
            }

            if (comm.ShortItemsAmount <= 0 || comm.ShortItemsAmount > CONST_MAX_UNREAD_NUMBER_OF_SHORTS_PER_USER_ACCOUNT)
            {
                msgs.Add(new Models.Message(LogLevel.Error, string.Format(CONST_VALUE_OUT_OF_RANGE, MatchShortUserAccountCommand.CONST_SHORT_ITEMS_AMOUNT_FIELDNAME, comm.ShortItemsAmount)));
            }

            if (comm.UserName == string.Empty)
            {
                msgs.Add(new Models.Message(LogLevel.Error, string.Format(CONST_VALUE_IS_EMPTY, MatchShortUserAccountCommand.CONST_USER_NAME_FIELDNAME, comm.UserName)));
            }

            if (!users.HasValue)
            {
                msgs.Add(new Models.Message(LogLevel.Error, string.Format("{0} value is {1}, not exist", MatchShortUserAccountCommand.CONST_USER_NAME_FIELDNAME, comm.UserName)));
            }

            return msgs;
        }

        private IEnumerable<Ad> AddAdsToMatch(MessageMatchShortUserAccountViewModel shortVM, UserAccount user)
        {
            int adsCountToMatch = DetermineAdsCountToMatch(shortVM);
            return _AdQueryBL.GetAdsFullData(user.LUSysInterfacelanguageKey, adsCountToMatch).Ads;
        }

        private int DetermineAdsCountToMatch(MessageMatchShortUserAccountViewModel shortVM)
        {
            return ((shortVM.Matches.Count() / CONST_NUMBER_OF_SHORTS_PER_AD) + 1);
        }

        private IEnumerable<Models.dbo.MessageMatchShortUserAccount> GetMatchData(MatchShortUserAccountCommand comm, Int64 user)
        {
            IDictionary<long, string> shorts = _shortQueryBL.GetShortsJsonData();
            IEnumerable<ShortUserAccount> suavm = _MatchAlgo.GetMatch(shorts, comm, user);
            IList<MessageMatchShortUserAccount> msg = new List<MessageMatchShortUserAccount>();

            Parallel.ForEach(suavm, (item) =>
            {
                if (shorts[item.ShortKey] != null)
                {
                    MessageMatchShortUserAccount json = JsonConvert.DeserializeObject<MessageMatchShortUserAccount>(shorts[item.ShortKey]);
                    json.UserAccountKey = user;
                    json.UserName = comm.UserName;
                    json.ShortSignAsLike = item.ShortSignAsLike;
                    json.ShortSignAsBookmark = item.ShortSignAsBookmark;
                    json.IsUserAccountWriterFollowed = item.UserSignWriterAsFollowed;
                    lock (thisLock)
                    {
                        msg.Add(json);
                    }
                }
            });

            if (comm.FirstShortKey.HasValue)
            {
                if (comm.FirstShortKey.Value > 0)
                    msg.Insert(0, JsonConvert.DeserializeObject<MessageMatchShortUserAccount>(shorts[comm.FirstShortKey.Value]));
            }

            if (msg.Count == 0)
            {
                msg.Insert(0, new Models.dbo.MessageMatchShortUserAccount() { IsAd = true, IsShort = false, UserName = comm.UserName, Quote = "No more Shorts today, be our guest tommorow (-:"  });
            }

            return msg;
        }
    }
}