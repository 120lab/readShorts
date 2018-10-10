using Framework.Core.Interfaces.CQRS;
using System;

namespace readShorts.Models.Commands
{
    public sealed class EventUserAccountCommand : ICommand
    {
        public string AdditionalData { get; set; }

        /// FK
        public Int64 UserAccountKey { get; set; }

        public Int64 LUEventTypeKey { get; set; }
    }
}