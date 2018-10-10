using System.Collections.Generic;
using readShorts.Entities.LOOKUP;
using System;

namespace readShorts.Entities.dbo
{

    public partial class ShortTag : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShortTag()
        {
        }
      
        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 LUShortTagTypeKey { get; set; }
        public Int64 ShortKey { get; set; }

    }
}
