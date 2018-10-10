using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class UpdateShortCommandHandler : CommandHandlerBase, ICommandHandler<UpdateShortsCommand, ShortViewModel>
    {
        public IShortCommandBL ShortCommandBL { get; private set; }

        public UpdateShortCommandHandler(IShortCommandBL shortCommandBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            ShortCommandBL = shortCommandBL;
        }

        public ShortViewModel Execute(UpdateShortsCommand command)
        {
            return ShortCommandBL.UpdateShort(command);
        }
    }
}
