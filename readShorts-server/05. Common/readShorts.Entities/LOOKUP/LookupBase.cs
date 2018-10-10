using System;

namespace readShorts.Entities.LOOKUP
{
    public class LookupBase
    {
        public Int64 RecordKey { get; set; }
        public string Description { get; set; }
        public Int64? LUSysInterfacelanguageKey { get; set; }
        public string AditionalData { get; set; }

    }
}
