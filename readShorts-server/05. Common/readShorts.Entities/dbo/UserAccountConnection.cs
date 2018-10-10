using System.Collections.Generic;
using readShorts.Entities.LOOKUP;
using System;

namespace readShorts.Entities.dbo
{

    public partial class UserAccountConnection : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccountConnection()
        {
        }
      
        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 FollowerUserAccountKey { get; set; }
        public Int64 FollowUserAccountKey { get; set; }
    }
}
