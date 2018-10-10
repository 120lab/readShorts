using System.Linq;
using readShorts.DataAccess.Interfaces;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.dbo
{
    public class AdQueryRepository : RepositoryBase<Ad>, IAdQueryRepository
    {
        public AdQueryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IQueryable<Ad> GetAds()
        {
            var res = (from c in dbset
                       select c).Where(x => x.IsRowDeleted == false);

            return res;
        }      
    }
}
