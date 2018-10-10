using System.Linq;

namespace readShorts.DataAccess.Interfaces
{
    using Framework.DataAccess.Interfaces;
    using Entities.dbo
        ;

    public interface IAdQueryRepository : IRepository<Ad>
    {
        IQueryable<Ad> GetAds();
    }
}
