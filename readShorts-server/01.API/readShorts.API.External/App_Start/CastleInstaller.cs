using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace readShorts.API.Public.IoC
{
    public class CastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               Classes
               .FromThisAssembly()
               //.FromAssemblyNamed("readShorts.API.External")
               .BasedOn<ApiController>()
               .LifestylePerWebRequest()
               );

            //// Infrastructure
            //container.Register(
            //    Component.For<IConfigurationService>().ImplementedBy<ConfigurationService>().LifestylePerWebRequest(),
            //    Component.For<IEmailService>().ImplementedBy<EmailService>().LifestylePerWebRequest(),
            //    Component.For<INetworkHttpClient>().ImplementedBy<NetworkHttpClient>().LifestylePerWebRequest(),
            //    Component.For<ILoggerService>().ImplementedBy<Log4NetLoggerService>().LifestylePerWebRequest(),
            //    Component.For<ILogConfig>().ImplementedBy<Log4NetConfig>().LifestyleSingleton()
            //    );

            //// CQRS
            //container.Register(
            //    Component.For<ICommandDispatcher>().ImplementedBy<CommandDispatcher>().LifestylePerWebRequest(),
            //    Component.For<IQueryDispatcher>().ImplementedBy<QueryDispatcher>().LifestylePerWebRequest()
            //    );                 


            //// Infrastructure
            //container.Register(Component.For<ILookupService>().ImplementedBy<LookupService>());

            //// Apartments
            //container.Register(Component.For<IApartmentService>().ImplementedBy<ApartmentService>());

            //var configString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\castleConfiguration.xml");            
            //container.Install(Castle.Windsor.Installer.Configuration.FromXmlFile(configString));
        }
    }
}