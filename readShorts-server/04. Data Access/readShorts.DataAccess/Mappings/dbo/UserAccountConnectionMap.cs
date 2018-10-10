using readShorts.Entities.dbo;

namespace readShorts.DataAccess.Mappings.dbo
{
    using System.Data.Entity.ModelConfiguration;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sealed class UserAccountConnectionMap : EntityTypeConfiguration<UserAccountConnection>
    {
        public UserAccountConnectionMap() 
        {
            // Primary Key
            HasKey(t => t.RecordKey);

            // Properties

            // Table & Column Mappings
            ToTable("UserAccountConnection");

            // Foreign Keys
            Property(t => t.FollowerUserAccountKey).HasColumnName("FollowerUserAccountKey");
            Property(t => t.FollowUserAccountKey).HasColumnName("FollowUserAccountKey");


            // Base columns
            Property(t => t.RecordKey).HasColumnName("RecordKey");
            Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp").HasColumnType("datetime2");
            
            Property(t => t.LastUpdateTimeStamp).HasColumnName("LastUpdateTimeStamp").HasColumnType("datetime2");
            Property(t => t.UniqueGuid).HasColumnName("UniqueGuid");
            Property(t => t.IsRowDeleted).HasColumnName("IsRowDeleted");

        }
    }
}

