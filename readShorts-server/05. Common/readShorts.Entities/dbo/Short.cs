using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;

namespace readShorts.Entities.dbo
{
    public partial class Short : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Short()
        {
        }

        /// <summary>
        /// Columns
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

        public Int64 LUSysInterfacelanguageKey { get; set; }
        public Int64 WriterUserKey { get; set; }
        public Int64 LUQuoteTypeKey { get; set; }
        public Int64 LUStoryTypeKey { get; set; }
        public Int64 LUCategoryTypeKey { get; set; }
    }
}