using Framework.Core.Interfaces.CQRS;
using System;

namespace readShorts.Models.Queries
{
    public sealed class ShortQuery : IQuery
    {
        public ShortQuery()
        {
            EnrichData = true;
        }

        public Int64 RecordKey { get; set; }
        public Int64 LUShortAgeRestrictionKey { get; set; }
        public bool EnrichData { get; set; }
    }
}
