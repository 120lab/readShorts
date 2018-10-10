using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Models.dbo;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace readShorts.BusinessLogic
{
    public class ShortUserAccountQueryBL : BaseBL, IShortUserAccountQueryBL
    {
        private readonly IShortUserAccountQueryRepository _shortUserAccountRepository;
        private readonly IUserQueryBL _userQueryBL;

        public ShortUserAccountQueryBL(IShortUserAccountQueryRepository shortUserAccountRepository, IUserQueryBL userQueryBL, IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _shortUserAccountRepository = shortUserAccountRepository;
            _userQueryBL = userQueryBL;    
        }

        public ShortUserAccountViewModel GetShortsUserAccount(ShortUserAccountQuery query)
        {
            IEnumerable<Entities.dbo.ShortUserAccount> result = null;
            ShortUserAccountViewModel shortVM = new ShortUserAccountViewModel();
            Int64? user = _userQueryBL.GetUser(query.UserName);

            if (!user.HasValue)
                return shortVM;

            switch (query.ShortsFeedTypeItem)
            {
                case readShorts.Models.Enums.ShortsFeedType.Home:
                    result = _shortUserAccountRepository.GetShortUserAccountsNotViewed(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.Bookmarked:
                    result = _shortUserAccountRepository.GetShortUserAccountsBookmarked(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.FollowedWriters:
                    result = _shortUserAccountRepository.GetShortUserAccountsFollowedWriters(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.TopLikes:
                    result = _shortUserAccountRepository.GetShortUserAccountsTopLiked(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.Liked:
                    result = _shortUserAccountRepository.GetShortUserAccountsLiked(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.FriendsShares:
                    result = _shortUserAccountRepository.GetShortUserAccountsFriendsShares(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.AllKnownRecords:
                    result = _shortUserAccountRepository.GetShortUserAccounts(user.Value);
                    break;

                case readShorts.Models.Enums.ShortsFeedType.ViewedOnly:
                    result = _shortUserAccountRepository.GetShortUserAccountsViewed(user.Value);
                    break;

                default:
                    result = _shortUserAccountRepository.GetShortUserAccounts(user.Value);
                    break;
            }

            if (result != null)
            {
                shortVM.ShortUserAccounts = base.Map<IEnumerable<Entities.dbo.ShortUserAccount>, IEnumerable<Models.dbo.ShortUserAccount>>(result);
            }
            return shortVM;
        }

        public IEnumerable<ShortUserAccount> GetShortsUserAccount(DateTime interval)
        {
            return base.Map<IEnumerable<Entities.dbo.ShortUserAccount>, IEnumerable<Models.dbo.ShortUserAccount>>(
                _shortUserAccountRepository.GetShortUserAccounts(interval));            
        }
    }
}