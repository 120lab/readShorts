using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Repositories.commands
{
    public class EventUserAccountCommandRepository : RepositoryBase<EventUserAccount>, IEventUserAccountCommandRepository
    {
        public EventUserAccountCommandRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}