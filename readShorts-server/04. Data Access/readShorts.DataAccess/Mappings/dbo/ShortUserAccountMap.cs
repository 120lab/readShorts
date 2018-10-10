using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class ShortUserAccountMap : EntityTypeConfiguration<ShortUserAccount>
    {
        public ShortUserAccountMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.ShortSendToUserTimeStamp).IsOptional();
            Property(t => t.ShortViewByUserTimeStamp).IsOptional();
            Property(t => t.ShortReadByUserTimeStamp).IsOptional();
            Property(t => t.ShortSignAsLikeTimeStamp).IsOptional();
            Property(t => t.ShortSignAsBookmarkTimeStamp).IsOptional();
            Property(t => t.UserSignNextShortTimeStamp).IsOptional();
            Property(t => t.UserSignWriterAsFollowedTimeStamp).IsOptional();

            // Table & Column Mappings
            ToTable("ShortUserAccount");

            Property(t => t.ShortSendToUser).HasColumnName("ShortSendToUser");
            Property(t => t.ShortSendToUserTimeStamp).HasColumnName("ShortSendToUserTimeStamp");
            Property(t => t.ShortViewByUser).HasColumnName("ShortViewByUser");
            Property(t => t.ShortViewByUserTimeStamp).HasColumnName("ShortViewByUserTimeStamp");
            Property(t => t.ShortReadByUser).HasColumnName("ShortReadByUser");
            Property(t => t.ShortReadByUserTimeInMiliSeconds).HasColumnName("ShortReadByUserTimeInMiliSeconds");
            Property(t => t.ShortReadByUserTimeStamp).HasColumnName("ShortReadByUserTimeStamp");
            Property(t => t.ShortSignAsLike).HasColumnName("ShortSignAsLike");
            Property(t => t.ShortSignAsLikeTimeStamp).HasColumnName("ShortSignAsLikeTimeStamp");
            Property(t => t.ShortSignAsBookmark).HasColumnName("ShortSignAsBookmark");
            Property(t => t.ShortSignAsBookmarkTimeStamp).HasColumnName("ShortSignAsBookmarkTimeStamp");
            Property(t => t.UserSignNextShort).HasColumnName("UserSignNextShort");
            Property(t => t.UserSignNextShortTimeStamp).HasColumnName("UserSignNextShortTimeStamp");
            Property(t => t.UserSignWriterAsFollowed).HasColumnName("UserSignWriterAsFollowed");
            Property(t => t.UserSignWriterAsFollowedTimeStamp).HasColumnName("UserSignWriterAsFollowedTimeStamp");

            // Foreign Keys
            Property(t => t.UserAccountKey).HasColumnName("UserAccountKey");
            Property(t => t.ShortKey).HasColumnName("ShortKey");

            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");
        }
    }
}