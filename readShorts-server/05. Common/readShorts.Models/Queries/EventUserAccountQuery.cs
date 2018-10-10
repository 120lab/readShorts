using Framework.Core.Interfaces.CQRS;
using System;

namespace readShorts.Models.Queries
{
    public sealed class EventUserAccountQuery : IQuery
    {        
        /// FK
        public Int64 UserAccoutKey { get; set; }        
    }
}
