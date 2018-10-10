using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Core.Interfaces.CQRS;
using readShorts.Services.Interfaces;

namespace readShorts.Services
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(
            //    //Component.For<ILookupService>().ImplementedBy<LookupService>(),         // Lookup
            //    Component.For<IApartmentService>().ImplementedBy<ApartmentService>(),   // Apartments
            //    Component.For<IProjectService>().ImplementedBy<ProjectService>(),        // Projects
            //    Component.For<IApplicantDetailsMessageService>().ImplementedBy<ApplicantDetailsMessageService>(),        // ApplicantDetailsService
            //    Component.For<ISiteApplicantsDetailsService>().ImplementedBy<SiteApplicantsDetailsService>(),        // SiteApplicantsDetailsService
            //    Component.For<IProjectToLotteryMessageService>().ImplementedBy<ProjectToLotteryMessageService>()        // ProjectToLotteryMessageService                                                                                                                        
            //    );

            // ServiceBase
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IServiceBase>()
                .LifestylePerWebRequest());
            
            // CQRS
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IQueryHandler<,>))
                .WithService.AllInterfaces());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(ICommandHandler<,>))
                .WithService.AllInterfaces());

        }
    }
}
