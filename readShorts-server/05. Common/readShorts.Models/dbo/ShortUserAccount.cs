namespace readShorts.Models.dbo
{
    using System;

    public partial class ShortUserAccount : DboBase
    {
        /// <summary>
        /// Columns
        /// </summary>
        public bool ShortSendToUser { get; set; }

        public DateTime? ShortSendToUserTimeStamp { get; set; }

        public bool ShortViewByUser { get; set; }
        public DateTime? ShortViewByUserTimeStamp { get; set; }

        public bool ShortReadByUser { get; set; }
        public int ShortReadByUserTimeInMiliSeconds { get; set; }
        public DateTime? ShortReadByUserTimeStamp { get; set; }

        public bool ShortSignAsLike { get; set; }
        public DateTime? ShortSignAsLikeTimeStamp { get; set; }

        public bool ShortSignAsBookmark { get; set; }
        public DateTime? ShortSignAsBookmarkTimeStamp { get; set; }

        public bool UserSignNextShort { get; set; }
        public DateTime? UserSignNextShortTimeStamp { get; set; }

        public bool UserSignWriterAsFollowed { get; set; }
        public DateTime? UserSignWriterAsFollowedTimeStamp { get; set; }

        /// FK
        public Int64 ShortKey { get; set; }

        public Int64 UserAccountKey { get; set; }
    }
}