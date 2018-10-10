namespace readShorts.Models.dbo
{
    using System;

    public partial class ShortBookmark : DboBase
    {
        /// <summary>
        /// Columns
        /// </summary>
        public bool IsShortAlreadyRead { get; set; }

        /// FK
        public Int64 UserAccoutKey { get; set; }
        public Int64 ShortKey { get; set; }

    }
}
