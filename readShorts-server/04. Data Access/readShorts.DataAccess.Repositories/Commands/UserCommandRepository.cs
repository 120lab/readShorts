using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{

    public class UserCommandRepository : RepositoryBase<UserAccount>, IUserCommandRepository
    {
        public UserCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }        
    }

}
