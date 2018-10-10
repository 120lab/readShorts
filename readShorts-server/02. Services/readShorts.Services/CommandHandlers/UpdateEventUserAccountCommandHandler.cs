using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Threading;

namespace readShorts.Services.CommandHandlers
{
    public sealed class UpdateEventUserAccountCommandHandler : CommandHandlerBase, ICommandHandler<EventUserAccountCommand, EventUserAccountViewModel>
    {
        public IEventUserAccountCommandBL _EventUserAccountCommandBL { get; private set; }

        public UpdateEventUserAccountCommandHandler(IEventUserAccountCommandBL EventUserAccountCommandBL, IUserQueryBL userQueryBL)
            : base(userQueryBL)
        {
            _EventUserAccountCommandBL = EventUserAccountCommandBL;
        }

        public EventUserAccountViewModel Execute(EventUserAccountCommand command)
        {
            return _EventUserAccountCommandBL.Add(command);
        }
    }
}