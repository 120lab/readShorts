using Framework.Core.Interfaces.CQRS;
using System;

namespace readShorts.Models.Queries
{
    public sealed class UserQuery : IQuery
    {
        public UserQuery()
        {
            EnrichData = true;
        }

        public Int64 RecordKey { get; set; }
        public string UserName { get; set; }
        public string Identity { get; set; }
        public string Password { get; set; }
        public string Guid { get; set; }
        public bool EnrichData { get; set; }
    }
}