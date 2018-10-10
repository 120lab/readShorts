using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class ShortTagMap : EntityTypeConfiguration<ShortTag>
    {
        public ShortTagMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties

            // Table & Column Mappings
            ToTable("ShortTag");

            // Foreign Keys
            Property(t => t.LUShortTagTypeKey).HasColumnName("LUShortTagTypeKey");
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