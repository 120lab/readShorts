using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class ShortMap : EntityTypeConfiguration<Short>
    {
        public ShortMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.Title)
                //.HasMaxLength(200)
                .IsUnicode(true);

            Property(t => t.CategoryPicturePath)
                //.HasMaxLength(2000)
                .IsUnicode(true);

            Property(t => t.BackgroundPicturePath)
                //.HasMaxLength(2000)
                .IsUnicode(true);

            Property(t => t.SharePicturePath)
                //.HasMaxLength(2000)
                .IsUnicode(true);

            Property(t => t.VideoPath)
                //.HasMaxLength(2000)
                .IsUnicode(true);

            Property(t => t.Embed)
                //.HasMaxLength(2000)
                .IsUnicode(true);

            Property(t => t.Quote)
                //.HasMaxLength(1000)
                .IsUnicode(true);

            Property(t => t.Text)
                //.HasMaxLength(3000)
                .IsUnicode(true);

            Property(t => t.JsonData)
                //.HasMaxLength(5000)
                .IsUnicode(true);

            // Table & Column Mappings
            ToTable("Short");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Quote).HasColumnName("Quote");
            Property(t => t.Text).HasColumnName("Text");
            Property(t => t.ERTInMiliSeconds).HasColumnName("ERTInMiliSeconds");
            Property(t => t.IsPublic).HasColumnName("IsPublic");

            Property(t => t.CategoryPicturePath).HasColumnName("CategoryPicturePath");
            Property(t => t.BackgroundPicturePath).HasColumnName("BackgroundPicturePath");
            Property(t => t.SharePicturePath).HasColumnName("SharePicturePath");
            Property(t => t.VideoPath).HasColumnName("VideoPath");
            Property(t => t.Embed).HasColumnName("Embed");
          //  Property(t => t.JsonData).HasColumnName("JsonData").HasColumnType("json");
            // Foreign Keys
            Property(t => t.LUShortAgeRestrictionKey).HasColumnName("LUShortAgeRestrictionKey");
            Property(t => t.LUSysInterfacelanguageKey).HasColumnName("LUSysInterfacelanguageKey");
            Property(t => t.WriterUserKey).HasColumnName("WriterUserKey");
            Property(t => t.LUQuoteTypeKey).HasColumnName("LUQuoteTypeKey");
            Property(t => t.LUStoryTypeKey).HasColumnName("LUStoryTypeKey");
            Property(t => t.LUCategoryTypeKey).HasColumnName("LUCategoryTypeKey");
            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");
        }
    }
}