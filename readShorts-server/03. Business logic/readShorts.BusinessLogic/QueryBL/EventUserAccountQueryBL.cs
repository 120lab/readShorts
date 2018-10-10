using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace readShorts.BusinessLogic
{
    public class EventUserAccountQueryBL : BaseBL, IEventUserAccountQueryBL
    {
        private readonly IEventUserAccountQueryRepository _EventUserAccountQueryRepository;

        public EventUserAccountQueryBL(IEventUserAccountQueryRepository EventUserAccountQueryRepository,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _EventUserAccountQueryRepository = EventUserAccountQueryRepository;
        }

        public EventUserAccountViewModel GetEventUserAccount(EventUserAccountQuery query)
        {
            IEnumerable<Entities.dbo.EventUserAccount> result = _EventUserAccountQueryRepository.GetEventUserAccount(query.UserAccoutKey);
            EventUserAccountViewModel shortVM = new EventUserAccountViewModel();
            shortVM.UserEvents = base.Map<IEnumerable<Entities.dbo.EventUserAccount>, IEnumerable<Models.dbo.EventUserAccount>>(result);

            return shortVM;
        }
    }
}