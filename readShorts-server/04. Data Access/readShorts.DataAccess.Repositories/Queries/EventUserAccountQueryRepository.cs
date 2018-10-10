using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Entities.dbo;
using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace readShorts.DataAccess.Repositories.Queries
{
    public class EventUserAccountQueryRepository : ReadOnlyRepositoryBase<EventUserAccount>, IEventUserAccountQueryRepository
    {
        public EventUserAccountQueryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<EventUserAccount> GetEventUserAccount(Int64 userAccountKey)
        {
            return dataContext.EventUserAccounts.Where(
                x => x.UserAccountKey == userAccountKey &&
                x.IsRowDeleted == false);
        }
    }
}