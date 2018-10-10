using System.Linq;

namespace readShorts.DataAccess.Interfaces
{
    using Framework.DataAccess.Interfaces;
    using Entities.dbo
        ;
    using System;
    using System.Collections.Generic;

    public interface IShortQueryRepository : IReadOnlyRepository<Short>
    {
        IEnumerable<Short> GetShort(Int64 recordKey);
        IEnumerable<Short> GetShorts();
        IEnumerable<Short> GetShortsByGetByAgeRestriction(Int64 lUShortAgeRestrictionKey);
    }
}
