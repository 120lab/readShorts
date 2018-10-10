using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.Services.QueryHandlers
{
    public sealed class ShortQueryHandler : ServiceBase, IQueryHandler<ShortQuery, ShortViewModel>
    {
        private readonly IShortQueryBL _shortQueryBL;

        public ShortQueryHandler(IShortQueryBL shortBL)
        {
            _shortQueryBL = shortBL;
        }

        public ShortViewModel Retrieve(ShortQuery query)
        {
            return _shortQueryBL.GetShorts(query);
        }
    }
}