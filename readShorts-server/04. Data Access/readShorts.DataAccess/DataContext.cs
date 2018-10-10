namespace readShorts.DataAccess
{
    using Entities.dbo;
    using Entities.LOOKUP;
    using System.Data.Entity;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=readShorts")
        {
            // for dev only
            // TBD
            //#if DEBUG
            //            Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
            //#endif
            Configuration.LazyLoadingEnabled = false;
        }

        /// DBO
        //public virtual DbSet<UserAccountActivity> UserAccountActivities { get; set; }
        //public virtual DbSet<UserAccountConnection> UserAccountConnections { get; set; }
        //public virtual DbSet<UserAccountGroup> UserAccountGroups { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Short> Shorts { get; set; }
        public virtual DbSet<EventUserAccount> EventUserAccounts { get; set; }
        public virtual DbSet<ShortTag> ShortTags { get; set; }
        public virtual DbSet<ShortUserAccount> ShortUserAccounts { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserAccountPoint> UserAccountPoints { get; set; }

        /// LOOKUP
        public virtual DbSet<LUActivity> LUActivities { get; set; }

        public virtual DbSet<LUCountry> LUCountries { get; set; }
        public virtual DbSet<LUEventType> LUEventTypes { get; set; }
        public virtual DbSet<LUGender> LUGenders { get; set; }
        public virtual DbSet<LUGroup> LUGroups { get; set; }
        public virtual DbSet<LUPointType> LUPointTypes { get; set; }
        public virtual DbSet<LUShortAgeRestriction> LUShortAgeRestrictions { get; set; }

        public virtual DbSet<LUShortCategoryType> LUShortCategoryTypes { get; set; }
        public virtual DbSet<LUShortReportType> LUShortReportTypes { get; set; }

        public virtual DbSet<LUShortShareType> LUShortShareTypes { get; set; }
        public virtual DbSet<LUShortTagType> LUShortTagTypes { get; set; }
        public virtual DbSet<LUSubscriptionType> LUSubscriptionTypes { get; set; }
        public virtual DbSet<LUSysInterfaceLanguage> LUSysInterfaceLanguages { get; set; }
        public virtual DbSet<LUQuoteType> LUQuoteType { get; set; }
        public virtual DbSet<LUStoryType> LUStoryType { get; set; }
        public virtual DbSet<LUUserVerificationType> LUUserVerificationType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /// DBO
            //modelBuilder.Configurations.Add(new Mappings.dbo.EventLogMap());
            //modelBuilder.Configurations.Add(new Mappings.dbo.UserAccountActivityMap());
            //modelBuilder.Configurations.Add(new Mappings.dbo.UserAccountConnectionMap());
            //modelBuilder.Configurations.Add(new Mappings.dbo.UserAccountGroupMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.AdMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.AuditMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.ShortMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.EventUserAccountMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.ShortTagMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.ShortUserAccountMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.UserAccountMap());
            modelBuilder.Configurations.Add(new Mappings.dbo.UserAccountPointMap());

            /// LOOKUP
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUActivityMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUCountryMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUEventTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUGenderMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUGroupMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUPointTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUShortAgeRestrictionMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUShortCategoryTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUShortReportTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUShortShareTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUShortTagTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUSubscriptionTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUSysInterfaceLanguageMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUQuoteTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUStoryTypeMap());
            modelBuilder.Configurations.Add(new Mappings.LOOKUP.LUUserVerificationTypeMap());
        }
    }
}