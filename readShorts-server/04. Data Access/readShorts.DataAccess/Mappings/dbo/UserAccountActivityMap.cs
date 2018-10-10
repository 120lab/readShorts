using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class UserAccountActivityMap : EntityTypeConfiguration<UserAccountActivity>
    {
        public UserAccountActivityMap() 
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.AdditionalDescription)
                .HasMaxLength(500)
                .IsUnicode(true);

            Property(e => e.AdditionalData)
                .IsUnicode(true)
                .HasColumnType("json")
                .HasMaxLength(2000);

            Property(t => t.Ident)
             .HasColumnType("uniqueidentifier")
             .HasColumnOrder(0);

            // Table & Column Mappings
            ToTable("UserAccountActivity");
            Property(t => t.AdditionalDescription).HasColumnName("AdditionalDescription");
            Property(t => t.AdditionalData).HasColumnName("AdditionalData");
            Property(t => t.Ident).HasColumnName("Ident");

            // Foreign keys
            Property(t => t.UserAccountKey).HasColumnName("UserAccountKey");
            Property(t => t.LUActivityKey).HasColumnName("LUActivityKey");

            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");

        }
    }
}

