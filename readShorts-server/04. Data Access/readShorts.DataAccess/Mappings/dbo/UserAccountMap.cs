using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class UserAccountMap : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountMap()
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties
            Property(t => t.UserSecurityNumber)
                .HasMaxLength(36)
                .IsUnicode(true);

            Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));

            Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            Property(e => e.ShortBio)
                .HasMaxLength(3000);

            Property(e => e.HashedPassword)
                .HasMaxLength(3000);

            Property(e => e.ClientIP)
                .HasMaxLength(100);

            Property(e => e.EmailAddress)
                .HasMaxLength(300)
                .IsRequired();

            Property(e => e.MobileSerialNumber)
                .HasMaxLength(100);



            Property(e => e.ExternalLink)
                .HasMaxLength(1000);

            Property(e => e.ExternalLinkText)
                .HasMaxLength(1000);

            Property(e => e.PersonalId)
                .HasMaxLength(36);

            Property(e => e.Address)
                .HasMaxLength(300);

            Property(e => e.MobilePhone)
                .HasMaxLength(36);

            // Table & Column Mappings
            ToTable("UserAccount");
            Property(t => t.UserSecurityNumber).HasColumnName("UserSecurityNumber");
            Property(t => t.UserName).HasColumnName("UserName");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.ShortBio).HasColumnName("ShortBio");
            Property(t => t.PicturePath).HasColumnName("PicturePath");
            Property(t => t.HashedPassword).HasColumnName("HashedPassword");
            Property(t => t.ClientIP).HasColumnName("ClientIP");
            Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            Property(t => t.MobileSerialNumber).HasColumnName("MobileSerialNumber");
            Property(t => t.LastActitiyDate).HasColumnName("LastActitiyDate");

            Property(t => t.IsAnonimousConnect).HasColumnName("IsAnonimousConnect");
            Property(t => t.IsFBConnect).HasColumnName("IsFBConnect");
            Property(t => t.IsTWConnect).HasColumnName("IsTWConnect");
            Property(t => t.IsGGLConnect).HasColumnName("IsGGLConnect");
            Property(t => t.IsEmailConnect).HasColumnName("IsEmailConnect");

            Property(t => t.BirthDate).HasColumnName("BirthDate");
            Property(t => t.EmailForShortIMightLike).HasColumnName("EmailForShortIMightLike");
            Property(t => t.EmailForShortOfTheWeek).HasColumnName("EmailForShortOfTheWeek");
            Property(t => t.EmailForShortFollowingWriter).HasColumnName("EmailForShortFollowingWriter");
            Property(t => t.EmailForNewSAndUpdates).HasColumnName("EmailForNewSAndUpdates");

            Property(t => t.ExternalLink).HasColumnName("ExternalLink");
            Property(t => t.ExternalLinkText).HasColumnName("ExternalLinkText");

            Property(t => t.PersonalId).HasColumnName("PersonalId");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.MobilePhone).HasColumnName("MobilePhone");

            // Foreign keys
            Property(t => t.LUSubscriptionTypeKey).HasColumnName("LUSubscriptionTypeKey");
            Property(t => t.LUSysInterfacelanguageKey).HasColumnName("LUSysInterfacelanguageKey");
            Property(t => t.LUGenderKey).HasColumnName("LUGenderKey");
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