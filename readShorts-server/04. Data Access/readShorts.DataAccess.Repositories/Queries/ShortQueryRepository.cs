using System.Linq;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.Queries
{

    public class ShortQueryRepository : ReadOnlyRepositoryBase<Short>, IShortQueryRepository
    {
        public ShortQueryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Short> GetShort(Int64 recordKey)
        {
            IEnumerable<Short> result = dataContext.Shorts.Where(x => x.RecordKey == recordKey && x.IsRowDeleted == false);

            return result;
        }

        public IEnumerable<Short> GetShorts()
        {
            IEnumerable<Short> result = from s in dataContext.Shorts
                                       where s.IsPublic == true &&
                                             s.IsRowDeleted == false                                             
                                       select s;
            return result;
        }

        public IEnumerable<Short> GetShortsByGetByAgeRestriction(Int64 lUShortAgeRestrictionKey)
        {
            IEnumerable<Short> result = from s in dataContext.Shorts
                                       where s.IsPublic == true &&
                                             s.IsRowDeleted == false &&
                                             s.LUShortAgeRestrictionKey <= lUShortAgeRestrictionKey
                                       select s;
            return result;
        }

    }

}
