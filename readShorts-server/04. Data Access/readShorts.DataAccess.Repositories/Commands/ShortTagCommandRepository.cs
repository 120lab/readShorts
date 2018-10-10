using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{
    public class ShortTagCommandRepository : RepositoryBase<ShortTag>, IShortTagCommandRepository
    {
        public ShortTagCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}