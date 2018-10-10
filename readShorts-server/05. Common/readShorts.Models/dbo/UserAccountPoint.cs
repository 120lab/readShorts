namespace readShorts.Models.dbo
{
    using System;

    public partial class UserAccountPoint : DboBase
    {
        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 UserAccountKey { get; set; }
        public Int64 LUPointTypeKey { get; set; }
    }
}
