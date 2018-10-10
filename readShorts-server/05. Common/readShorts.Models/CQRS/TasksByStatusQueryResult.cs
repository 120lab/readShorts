using System;
using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.CQRS
{
    public class TasksByStatusQueryResult : IQueryResult
    {
        public DateTime LastUpdateForAnyTask { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}