using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{

    public class ShortCommandRepository : RepositoryBase<Short>, IShortCommandRepository
    {
        public ShortCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }        
    }

}
