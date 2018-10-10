namespace readShorts.Models.dbo
{
    using System;

    public partial class UserAccountGroup : DboBase
    {

        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 UserAccountKey { get; set; }
        public Int64 LUGroupKey { get; set; }
    }
}
