using System.Collections.Generic;
using readShorts.Entities.LOOKUP;
using System;

namespace readShorts.Entities.dbo
{
    /// <summary>
    /// Entity use : Collect the user's points
    /// </summary>

    public partial class UserAccountPoint : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccountPoint()
        {
        }

        /// <summary>
        /// Columns
        /// </summary>
        public decimal Value { get; set; }

        /// FK
        public Int64 UserAccountKey { get; set; }
        public Int64 LUPointTypeKey { get; set; }
    }
}
