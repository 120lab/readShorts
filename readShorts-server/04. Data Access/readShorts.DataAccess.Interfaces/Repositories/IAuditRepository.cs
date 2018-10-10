using System.Linq;
using Framework.DataAccess.Interfaces;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Interfaces.Repositories
{
    public interface IAuditRepository : IRepository<Audit>
    {
        IQueryable<Audit> GetAll();
    }     
}