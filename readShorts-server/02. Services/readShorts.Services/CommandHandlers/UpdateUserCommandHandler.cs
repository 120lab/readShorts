using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class UpdateUserCommandHandler : CommandHandlerBase, ICommandHandler<UpdateUsersCommand, UserViewModel>
    {
        public IUserCommandBL UserCommandBL { get; private set; }

        public UpdateUserCommandHandler(IUserCommandBL userCommandBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            UserCommandBL = userCommandBL;
        }

        public UserViewModel Execute(UpdateUsersCommand command)
        {
            return UserCommandBL.UpdateUser(command);
        }
    }
}
