using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readShorts.Entities
{
    public abstract class EntityBase
    {
        public Int64 RecordKey { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public DateTime LastUpdateTimeStamp { get; set; }

        public bool IsRowDeleted { get; set; }

        public string UniqueGuid { get; set; }
    }
}