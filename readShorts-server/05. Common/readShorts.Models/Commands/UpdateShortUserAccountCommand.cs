using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using System;
using System.Collections.Generic;

namespace readShorts.Models.Commands
{
    public class UpdateShortUserAccountCommand : ICommand
    {
        public bool? ShortSendToUser { get; set; }
        public bool? ShortViewByUser { get; set; }
        public int ShortReadByUserTimeInMiliSeconds { get; set; }
        public bool? ShortSignAsLike { get; set; }
        public bool? ShortSignAsBookmark { get; set; }
        public bool? UserSignNextShort { get; set; }
        public bool? UserSignWriterAsFollowed { get; set; }

        /// FK
        public Int64 ShortKey { get; set; }

        public Int64 UserAccountKey { get; set; }
    }
}