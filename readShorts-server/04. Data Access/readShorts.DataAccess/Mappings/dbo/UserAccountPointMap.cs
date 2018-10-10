using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class UserAccountPointMap : EntityTypeConfiguration<UserAccountPoint>
    {
        public UserAccountPointMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Table & Column Mappings
            ToTable("UserAccountPoint");
            // Foreign keys
            Property(t => t.UserAccountKey).HasColumnName("UserAccountKey");
            Property(t => t.LUPointTypeKey).HasColumnName("LUPointTypeKey");

            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");
            Property(t => t.RecordKey).HasColumnName("UniqueGuid");
        }
    }
}