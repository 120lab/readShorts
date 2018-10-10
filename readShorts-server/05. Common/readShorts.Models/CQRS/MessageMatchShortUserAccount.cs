namespace readShorts.Models.dbo
{
    using System;
    using System.Collections.Generic;

    public partial class MessageMatchShortUserAccount
    {
        public MessageMatchShortUserAccount()
        {
            Tags = new List<Models.LOOKUP.LUShortTagType>();
        }

        /// keys
        public Int64 ShortKey { get; set; }

        public Int64 UserAccountKey { get; set; }

        /// <summary>
        /// Short properties
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Short + Ad properties
        /// </summary>
        public int Index { get; set; }

        public bool IsShort { get; set; }
        public bool IsAd { get; set; }

        public string Title { get; set; }
        public string Quote { get; set; }
        public string Text { get; set; }
        public int ERTInMiliSeconds { get; set; }
        public bool IsPublic { get; set; }

        public string CategoryPicturePath { get; set; }
        public string SharePicturePath { get; set; }
        public string BackgroundPicturePath { get; set; }
        public string VideoPath { get; set; }
        public string Embed { get; set; }

        /// FK
        public Int64 LUShortAgeRestrictionKey { get; set; }

        public string LUShortAgeRestrictionName { get; set; }
        public Int64 LUSysInterfacelanguageKey { get; set; }
        public string LUSysInterfacelanguageName { get; set; }
        public Int64 WriterUserKey { get; set; }
        public string WriterUserName { get; set; }
        public Int64 QuoteTypeKey { get; set; }
        public string QuoteTypeName { get; set; }
        public Int64 StoryTypeKey { get; set; }
        public string StoryTypeName { get; set; }
        public Int64 CategoryTypeKey { get; set; }
        public string CategoryTypeName { get; set; }

        /// <summary>
        /// Short UserAcount properties
        /// </summary>
        public bool ShortSignAsLike { get; set; }

        public bool ShortSignAsBookmark { get; set; }
        public bool IsUserAccountWriterFollowed { get; set; }

        /// <summary>
        /// Writers properties
        /// </summary>
        public string WriterFirstName { get; set; }

        public string WriterLastName { get; set; }
        public string WriterEmailAddress { get; set; }
        public string WriterShortBio { get; set; }
        public string WriterPicturePath { get; set; }
        public string WriterCountryName { get; set; }
        public string WriterExternalLink { get; set; }
        public string WriterExternalLinkText { get; set; }

        public IEnumerable<Models.LOOKUP.LUShortTagType> Tags { get; set; }
    }
}