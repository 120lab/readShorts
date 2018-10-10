using System;
using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.CQRS;

namespace readShorts.Models.Queries.Results
{
    public class TasksByStatusQueryResult : IQueryResult
    {
        public DateTime LastUpdateForAnyTask { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}