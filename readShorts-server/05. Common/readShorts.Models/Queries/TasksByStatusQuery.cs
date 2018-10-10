using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.Queries
{
    public class TasksByStatusQuery : IQuery
    {
        public bool IsCompleted { get; set; }
    }
}