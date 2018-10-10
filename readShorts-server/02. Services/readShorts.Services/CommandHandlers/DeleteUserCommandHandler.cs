using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Threading;

namespace readShorts.Services.CommandHandlers
{
    public sealed class DeleteUserCommandHandler : CommandHandlerBase, ICommandHandler<DeleteUsersCommand, UserViewModel>
    {
        public IUserCommandBL UserCommandBL { get; private set; }

        public DeleteUserCommandHandler(IUserCommandBL userCommandBL, IUserQueryBL userQueryBL)
            : base(userQueryBL)
        {
            UserCommandBL = userCommandBL;
        }

        public UserViewModel Execute(DeleteUsersCommand command)
        {
            return UserCommandBL.DeleteUser(command);
        }
    }
}