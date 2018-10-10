using System.Linq;

namespace readShorts.DataAccess.Interfaces
{
    using Framework.DataAccess.Interfaces;
    using Entities.dbo
        ;
    using System;
    using System.Collections.Generic;

    public interface IEventUserAccountQueryRepository : IReadOnlyRepository<EventUserAccount>
    {
        IEnumerable<EventUserAccount> GetEventUserAccount( Int64 userAccountKey);
 
    }
}
