using Framework.DataAccess.Interfaces;
using readShorts.Entities.dbo;
using readShorts.Entities.LOOKUP;

namespace readShorts.DataAccess.Repositories.Interfaces.Commands
{
    public interface ILookupCommandRepository : IRepository<LookupBase>
    {
        void AddLUShortCategoryType(LookupBase obj);

        void AddLUShortTagType(LookupBase obj);
    }
}