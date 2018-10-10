using Framework.DataAccess.Interfaces;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.Interfaces.Commands
{
    public interface IAuditCommandRepository : IRepository<Audit>
    {
    }
}
