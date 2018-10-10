using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{

    public class ShortUserAccountCommandRepository : RepositoryBase<ShortUserAccount>, IShortUserAccountCommandRepository
    {
        public ShortUserAccountCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }        
    }

}
