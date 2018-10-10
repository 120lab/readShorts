using System.Collections.Generic;
using readShorts.Entities.LOOKUP;
using System;

namespace readShorts.Entities.dbo
{
    /// <summary>
    /// Entity use : Indicates the groups the user own to
    /// </summary>
    public partial class UserAccountGroup : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccountGroup()
        {
        }
      
        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 UserAccountKey { get; set; }
        public Int64 LUGroupKey { get; set; }
    }
}
