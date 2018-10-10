using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class EventUserAccountMap : EntityTypeConfiguration<EventUserAccount>
    {
        public EventUserAccountMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.AdditionalData)
                .IsUnicode(true)
                // .HasColumnType("json")
                .HasMaxLength(2000);

            // Table & Column Mappings
            ToTable("EventUserAccount");

            Property(t => t.AdditionalData).HasColumnName("AdditionalData");
            // Foreign Keys
            Property(t => t.LUEventTypeKey).HasColumnName("LUEventTypeKey");
            Property(t => t.UserAccountKey).HasColumnName("UserAccountKey");

            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");
        }
    }
}