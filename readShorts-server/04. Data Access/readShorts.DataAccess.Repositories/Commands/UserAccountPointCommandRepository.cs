using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{

    public class UserAccountPointCommandRepository : RepositoryBase<UserAccountPoint>, IUserAccountPointCommandRepository
    {
        public UserAccountPointCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }        
    }

}
