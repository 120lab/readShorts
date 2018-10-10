using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Core.CQRS;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Interfaces.Log;
using Framework.Core.Interfaces.Utils;
using Framework.Core.Log;
using readShorts.API.Services;

namespace readShorts.API.Internal.IoC
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Infrastructure
            container.Register(
                Component.For<IConfigurationService>().ImplementedBy<ConfigurationService>().LifestylePerWebRequest(),
                //Component.For<IEmailService>().ImplementedBy<EmailService>().LifestylePerWebRequest(),
                //Component.For<INetworkHttpClient>().ImplementedBy<NetworkHttpClient>().LifestylePerWebRequest(),
                Component.For<ILoggerService>().ImplementedBy<Log4NetLoggerService>().LifestylePerWebRequest(),
                Component.For<ILogConfig>().ImplementedBy<Log4NetConfig>().LifestylePerWebRequest()
                );

            // CQRS
            container.Register(
                Component.For<ICommandDispatcher>().ImplementedBy<CommandDispatcher>().LifestylePerWebRequest(),
                Component.For<IQueryDispatcher>().ImplementedBy<QueryDispatcher>().LifestylePerWebRequest()
                );


            // CQRS
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IQueryHandler<,>))
                .WithService.AllInterfaces());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(ICommandHandler<,>))
                .WithService.AllInterfaces());
            //// Infrastructure
            //container.Register(Component.For<ILookupService>().ImplementedBy<LookupService>());

            //// Apartments
            //container.Register(Component.For<IApartmentService>().ImplementedBy<ApartmentService>());

            //var configString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\castleConfiguration.xml");            
            //container.Install(Castle.Windsor.Installer.Configuration.FromXmlFile(configString));
        }
    }
}