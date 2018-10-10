using System;

namespace readShorts.Models
{
    public abstract class ModelBase
    {
        public Int64 RecordKey { get; set; }
        public bool IsRowDeleted { get; set; }
        
    }

}
