using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.Services.QueryHandlers
{
    public sealed class EventUserAccountQueryHandler : ServiceBase, IQueryHandler<EventUserAccountQuery, EventUserAccountViewModel>
    {
        private readonly IEventUserAccountQueryBL _EventUserAccountQueryBL;

        public EventUserAccountQueryHandler(IEventUserAccountQueryBL EventUserAccountQueryBL)
        {
            _EventUserAccountQueryBL = EventUserAccountQueryBL;
        }

        public EventUserAccountViewModel Retrieve(EventUserAccountQuery query)
        {
                return _EventUserAccountQueryBL.GetEventUserAccount(query);
        }
    }
}