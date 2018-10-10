using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class AdMap : EntityTypeConfiguration<Ad>
    {
        public AdMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.CompanyName).HasMaxLength(200).IsUnicode(true);
            Property(t => t.AdPath).HasMaxLength(2000).IsUnicode(true);
            Property(t => t.AdBody).HasMaxLength(3000).IsUnicode(true);
            Property(t => t.LUCountryKey).IsOptional();
            Property(t => t.LUShortAgeRestrictionKey).IsOptional();
            Property(t => t.LUSysInterfacelanguageKey).IsOptional();

            // Table & Column Mappings
            ToTable("Ad");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.Interval).HasColumnName("Interval");
            Property(t => t.AdPath).HasColumnName("AdPath");
            Property(t => t.AdBody).HasColumnName("AdBody");

            /// FK
            Property(t => t.LUShortAgeRestrictionKey).HasColumnName("LUShortAgeRestrictionKey");
            Property(t => t.LUSysInterfacelanguageKey).HasColumnName("LUSysInterfacelanguageKey");
            Property(t => t.LUCountryKey).HasColumnName("LUCountryKey");

            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");
        }
    }
}