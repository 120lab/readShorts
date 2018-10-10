namespace readShorts.Models.dbo
{
    using System;

    public partial class Short : DboBase
    {
        /// </summary>
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
        public string JsonData { get; set; }

        /// FK
        public Int64 LUShortAgeRestrictionKey { get; set; }

        public string LUShortAgeRestrictionName { get; set; }
        public Int64 LUSysInterfacelanguageKey { get; set; }
        public string LUSysInterfacelanguageName { get; set; }
        public Int64 WriterUserKey { get; set; }
        public string WriterUserName { get; set; }
        public string WriterLastName { get; set; }
        public string WriterFirstName { get; set; }
        public Int64 LUQuoteTypeKey { get; set; }
        public string LUQuoteTypeName { get; set; }
        public Int64 LUStoryTypeKey { get; set; }
        public string LUStoryTypeName { get; set; }
        public Int64 LUCategoryTypeKey { get; set; }
        public string LUCategoryTypeName { get; set; }
    }
}