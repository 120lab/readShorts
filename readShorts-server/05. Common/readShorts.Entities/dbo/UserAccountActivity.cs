using System.Collections.Generic;
using readShorts.Entities.LOOKUP;
using System;

namespace readShorts.Entities.dbo
{

    public partial class UserAccountActivity : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccountActivity()
        {
        }
      
        /// <summary>
        /// Columns
        /// </summary>
        public string AdditionalDescription { get; set; }
        public string AdditionalData { get; set; }
        public Guid Ident { get; set; }
        /// FK
        public Int64 UserAccountKey { get; set; }        
        public Int64 LUActivityKey { get; set; }

}
}
