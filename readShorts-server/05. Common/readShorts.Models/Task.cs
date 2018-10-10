using System;
using Framework.Core.Interfaces.CQRS;

namespace readShorts.Services.CQRS.Aggregate
{
    public class Task : IEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted;
        public DateTime LastUpdated;
    }
}