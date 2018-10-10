using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.Services.QueryHandlers
{
    public sealed class ShortUserAccountQueryHandler : ServiceBase, IQueryHandler<ShortUserAccountQuery, ShortUserAccountViewModel>
    {
        private readonly IShortUserAccountQueryBL _shortQueryBL;

        public ShortUserAccountQueryHandler(IShortUserAccountQueryBL shortBL)
        {
            _shortQueryBL = shortBL;
        }

        public ShortUserAccountViewModel Retrieve(ShortUserAccountQuery query)
        {
            if (query.UserName != null)
                return _shortQueryBL.GetShortsUserAccount(query);

            return null;
        }
    }
}