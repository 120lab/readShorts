using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Interfaces;

//using readShorts.DataAccess.Interfaces.Repositories; bpini
using readShorts.DataAccess.Repositories;
using readShorts.DataAccess.Repositories.commands;
using readShorts.DataAccess.Repositories.dbo;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.DataAccess.Repositories.Queries;

namespace readShorts.BusinessLogic
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Queries components
            container.Register(
                Component.For<IUserQueryBL>().ImplementedBy<UserQueryBL>(),                     // Users
                Component.For<ILookupQueryBL>().ImplementedBy<LookupQueryBL>(),                  // Lookup
                Component.For<IShortQueryBL>().ImplementedBy<ShortQueryBL>(),                  // Short
                Component.For<IShortUserAccountQueryBL>().ImplementedBy<ShortUserAccountQueryBL>(),                  // ShortUserAccountQuery
                Component.For<IAdQueryBL>().ImplementedBy<AdQueryBL>(),                  // adQuery
                Component.For<IEventUserAccountQueryBL>().ImplementedBy<EventUserAccountQueryBL>()                  // EventUserAccountQueryBL
                );

            // Command components
            container.Register(
                Component.For<IShortCommandBL>().ImplementedBy<ShortCommandBL>(),  // Shorts
                Component.For<IShortTagCommandBL>().ImplementedBy<ShortTagCommandBL>(),  // ShortsTag
                Component.For<IUserCommandBL>().ImplementedBy<UserCommandBL>(),  // Users
                Component.For<IAuditCommandBL>().ImplementedBy<AuditCommandBL>(),  // Audit
                Component.For<IShortUserAccountCommandBL>().ImplementedBy<ShortUserAccountCommandBL>(),// ShortUserAccountCommand
                Component.For<IUserAccountPointCommandBL>().ImplementedBy<UserAccountPointCommandBL>(),  // IUserAccountPointCommandBL
                Component.For<IEventUserAccountCommandBL>().ImplementedBy<EventUserAccountCommandBL>(),  // EventUserAccountCommandBL
                Component.For<ILookupCommandBL>().ImplementedBy<LookupCommandBL>()  // ILookupCommandBL
                );

            // Logical components
            container.Register(
                Component.For<IMatchAlgoBL>().ImplementedBy<MatchAlgoBL>(),  // IMatchAlgoBL
                Component.For<IMatchShortUserAccountBL>().ImplementedBy<MatchShortUserAccountBL>(),  // MatchShortUserAccount
                Component.For<IGeneralTasksBL>().ImplementedBy<GeneralTasksBL>()  // UserAccountBehaviorBL
                );

        // Repositories
        container.Register(
                Component.For<IDatabaseFactory>().ImplementedBy<DatabaseFactory>(),         // Database Factory
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>(),                // Unit of Work
                Component.For<IUserAccountQueryRepository>().ImplementedBy<UserAccountRepository>(),          // UserAccountRepository
                Component.For<IUserCommandRepository>().ImplementedBy<UserCommandRepository>(),          // UserAccountRepository
                Component.For<ILookupQueryRepository>().ImplementedBy<LookupQueryRepository>(),          // LookupQueryRepository
                Component.For<ILookupCommandRepository>().ImplementedBy<LookupCommandRepository>(),          //
                Component.For<IAuditCommandRepository>().ImplementedBy<AuditCommandRepository>(),          // AuditCommandRepository
                Component.For<IShortQueryRepository>().ImplementedBy<ShortQueryRepository>(),          // ShortQueryRepository
                Component.For<IShortUserAccountQueryRepository>().ImplementedBy<ShortUserAccountQueryRepository>(),   // ShortUserAccountQueryRepository
                Component.For<IShortUserAccountCommandRepository>().ImplementedBy<ShortUserAccountCommandRepository>(), // ShortUserAccountCommandRepository
                Component.For<IAdQueryRepository>().ImplementedBy<AdQueryRepository>(), // AdCommandRepository
                Component.For<IUserAccountPointCommandRepository>().ImplementedBy<UserAccountPointCommandRepository>(), // UserAccountPointCommandRepository
                Component.For<IEventUserAccountCommandRepository>().ImplementedBy<EventUserAccountCommandRepository>(), // EventUserAccountCommandRepository
                Component.For<IEventUserAccountQueryRepository>().ImplementedBy<EventUserAccountQueryRepository>(), // EventUserAccountQueryRepository
                Component.For<IShortCommandRepository>().ImplementedBy<ShortCommandRepository>(),
                Component.For<IShortTagCommandRepository>().ImplementedBy<ShortTagCommandRepository>()
                );

            // BaseBL
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IBaseBL>()
                .LifestylePerWebRequest());
        }
    }
}