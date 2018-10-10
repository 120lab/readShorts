using readShorts.BusinessLogic.Interfaces;

namespace readShorts.Services.CommandHandlers
{
    public abstract class CommandHandlerBase : ServiceBase
    {
        public IUserQueryBL UserQueryBL { get; private set; }

        public CommandHandlerBase(IUserQueryBL userQueryBL)
        {
            UserQueryBL = userQueryBL;
        }
    }
}
