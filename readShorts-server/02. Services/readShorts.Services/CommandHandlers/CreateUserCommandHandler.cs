using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class CreateUserCommandHandler : CommandHandlerBase, ICommandHandler<CreateUserCommand, UserViewModel>
    {
        public IUserCommandBL UserCommandBL { get; private set; }

        public CreateUserCommandHandler(IUserCommandBL userCommandBL, IUserQueryBL userQueryBL)
            : base(userQueryBL)
        {
            UserCommandBL = userCommandBL;
        }

        public UserViewModel Execute(CreateUserCommand command)
        {
            return UserCommandBL.CreateUser(command);
        }
    }
}