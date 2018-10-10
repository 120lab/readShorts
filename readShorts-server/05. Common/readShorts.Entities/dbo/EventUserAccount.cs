using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;

namespace readShorts.Entities.dbo
{
    public partial class EventUserAccount : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventUserAccount()
        {
        }

        public string AdditionalData { get; set; }

        /// FK
        public Int64 UserAccountKey { get; set; }

        public Int64 LUEventTypeKey { get; set; }
    }
}