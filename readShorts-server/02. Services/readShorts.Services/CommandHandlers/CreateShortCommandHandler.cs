using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class CreateShortCommandHandler : CommandHandlerBase, ICommandHandler<CreateShortCommand, ShortViewModel>
    {
        public IShortCommandBL ShortCommandBL { get; private set; }

        public CreateShortCommandHandler(IShortCommandBL shortCommandBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            ShortCommandBL = shortCommandBL;
        }

        public ShortViewModel Execute(CreateShortCommand command)
        {
            return ShortCommandBL.CreateShort(command);
        }
    }
}
