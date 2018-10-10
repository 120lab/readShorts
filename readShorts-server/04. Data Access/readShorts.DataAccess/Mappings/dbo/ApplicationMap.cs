using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class ApplicationMap : EntityTypeConfiguration<Application>
    {
        public ApplicationMap()
        {
            // Primary Key
            HasKey(t => t.ApplicationNumber);

            // Properties
            //Property(t => t.EnrollmentNumber)
                //.HasColumnType("uniqueidentifier");
            
            //Property(e => e.LastName)
            //    .IsRequired()
            //    .HasMaxLength(50)
            //    .IsUnicode(false);

            Property(e => e.FirstApplicantIdentityNumber)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false);

            Property(e => e.SecondApplicantIdentityNumber)
                .HasMaxLength(9)
                .IsUnicode(false);

            Property(e => e.MainPhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            Property(e => e.CityCode)
                .IsRequired();

            Property(e => e.StreetCode)
                .IsRequired();

            Property(e => e.HouseNumber)
                .HasMaxLength(5)
                .IsRequired()
                .IsUnicode(false);

            Property(e => e.ZipCode)
                .HasMaxLength(7)
                .IsRequired()
                .IsUnicode(false);

            Property(e => e.PO_Box)
                .HasMaxLength(7)
                .IsUnicode(false);

            Property(e => e.AddressDescription)
                .HasMaxLength(255)
                .IsUnicode(false);

            Property(e => e.Email)
                .HasMaxLength(50);

            Property(e => e.PasswordHash)
                .HasMaxLength(100);
            
            Property(t => t.CreatedUserId)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false);

            Property(t => t.LastUpdateUserId)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false);

            // Table & Column Mappings
            ToTable("Applications");
            Property(t => t.ApplicationNumber).HasColumnName("ApplicationNumber");
            Property(t => t.PersonalStatusCode).HasColumnName("PersonalStatusCode");
            Property(t => t.FirstApplicantIdentityNumber).HasColumnName("FirstApplicantIdentityNumber");
            Property(t => t.SecondApplicantIdentityNumber).HasColumnName("SecondApplicantIdentityNumber");
            Property(t => t.MainPhoneAreaCode).HasColumnName("MainPhoneAreaCode");
            Property(t => t.MainPhoneNumber).HasColumnName("MainPhoneNumber");
            Property(t => t.CityCode).HasColumnName("CityCode");
            Property(t => t.StreetCode).HasColumnName("StreetCode");
            Property(t => t.HouseNumber).HasColumnName("HouseNumber");
            Property(t => t.ZipCode).HasColumnName("ZipCode");
            Property(t => t.PO_Box).HasColumnName("PO_Box");
            Property(t => t.AddressDescription).HasColumnName("AddressDescription");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.EnrollmentStatusCode).HasColumnName("EnrollmentStatusCode");
            Property(t => t.IsAllowSMS).HasColumnName("IsAllowSMS");
            Property(t => t.RemindersCounter).HasColumnName("RemindersCounter");
            Property(t => t.Notes).HasColumnName("Notes");
            Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            Property(t => t.IsAllowedTargetPrice).HasColumnName("IsAllowedTargetPrice");
            Property(t => t.IsAllowedTenantPrice).HasColumnName("IsAllowedTenantPrice");
            Property(t => t.IsAllowedFutureParticipant1).HasColumnName("IsAllowedFutureParticipant1");
            Property(t => t.IsAllowedFutureParticipant2).HasColumnName("IsAllowedFutureParticipant2");
            Property(t => t.IsHomeless).HasColumnName("IsHomeless");
            Property(t => t.TenantPriceEligibilityTypeCode).HasColumnName("TenantPriceEligibilityTypeCode");
            Property(t => t.TargetPriceEligibilityTypeCode).HasColumnName("TargetPriceEligibilityTypeCode");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
            Property(t => t.LastUpdateUserId).HasColumnName("LastUpdateUserId");
            Property(t => t.SortOrder).HasColumnName("SortOrder");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");

            // Relationships
            HasRequired(t => t.CreatedUser)
                .WithMany(t => t.ApplicationsCreatedUser)
                .HasForeignKey(d => d.CreatedUserId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.UpdatedUser)
                .WithMany(t => t.ApplicationsUpdatedUser)
                .HasForeignKey(d => d.LastUpdateUserId)
                .WillCascadeOnDelete(false);
            
        }
    }
}

