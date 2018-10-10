namespace readShorts.DataAccess.Mappings.LOOKUP
{

    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public sealed class LUShortShareTypeMap : EntityTypeConfiguration<readShorts.Entities.LOOKUP.LUShortShareType>
    {
        public LUShortShareTypeMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);
            Property(x => x.RecordKey)
               .HasColumnOrder(0);

            // Properties
            Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnOrder(1)
                .IsUnicode(true);

            Property(x => x.LUSysInterfacelanguageKey)
                .HasColumnOrder(2);

            Property(x => x.AditionalData)
                .HasMaxLength(3000)
                .HasColumnOrder(3)
                .IsUnicode(true);

            // Table & Column Mappings
            ToTable("LOOKUP.LUShortShareType");
            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.LUSysInterfacelanguageKey).HasColumnName("LUSysInterfacelanguageKey");
            Property(t => t.AditionalData).HasColumnName("AditionalData");



        }
    }
}