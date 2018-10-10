namespace readShorts.Models.dbo
{
    using System;

    public partial class UserAccountActivity : DboBase
    {

        /// <summary>
        /// Columns
        /// </summary>
        public string AdditionalDescription { get; set; }
        public string AdditionalData { get; set; }

        /// FK
        public Int64 UserAccountKey { get; set; }
        public Int64 LUActivityKey { get; set; }
        public string LUActivityName { get; set; }
    }
}
