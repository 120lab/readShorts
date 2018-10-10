using System;
using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.CQRS
{
    public class ChangeTaskStatusCommand : ICommand 
    {
        public string TaskId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}