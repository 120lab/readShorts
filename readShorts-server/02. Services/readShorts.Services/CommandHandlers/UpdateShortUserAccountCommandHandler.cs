using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class UpdateShortUserAccountCommandHandler : CommandHandlerBase, ICommandHandler<UpdateShortUserAccountCommand, ShortUserAccountViewModel>
    {
        public IShortUserAccountCommandBL _shortUserAccountCommandBL { get; private set; }

        public UpdateShortUserAccountCommandHandler(IShortUserAccountCommandBL shortUserAccountCommandBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            _shortUserAccountCommandBL = shortUserAccountCommandBL;
        }

        public ShortUserAccountViewModel Execute(UpdateShortUserAccountCommand command)
        {
            return _shortUserAccountCommandBL.UpdateShortUserAccount(command);
        }
    }
}
