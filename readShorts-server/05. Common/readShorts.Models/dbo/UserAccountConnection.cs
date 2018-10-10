namespace readShorts.Models.dbo
{
    using System;

    public partial class UserAccountConnection : DboBase
    {

        /// <summary>
        /// Columns
        /// </summary>

        /// FK
        public Int64 FollowerUserAccountKey { get; set; }
        public Int64 FollowUserAccountKey { get; set; }
    }
}
