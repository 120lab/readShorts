using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.CQRS
{
    public class TasksByStatusQuery : IQuery
    {
        public bool IsCompleted { get; set; }
    }
}