using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;
using readShorts.Entities.LOOKUP;

namespace readShorts.DataAccess.Repositories.commands
{
    public class LookupCommandRepository : RepositoryLookupBase<LookupBase>, ILookupCommandRepository
    {
        public LookupCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public void AddLUShortCategoryType(LookupBase obj)
        {
            dataContext.LUShortCategoryTypes.Add(new LUShortCategoryType(obj));
        }

        public void AddLUShortTagType(LookupBase obj)
        {
            dataContext.LUShortTagTypes.Add(new LUShortTagType(obj));
        }
    }
}