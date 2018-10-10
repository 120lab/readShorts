using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{

    public class AuditCommandRepository : RepositoryBase<Audit>, IAuditCommandRepository
    {
        public AuditCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }        
    }

}
