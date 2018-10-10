using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Core.Interfaces.CQRS;
using readShorts.Services.Interfaces;

namespace readShorts.Services.Bootstrapper
{
    using BusinessLogicBootstrapper = BusinessLogic.Bootstrapper.CastleInstaller;

    public sealed class CastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // ServiceBase
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IServiceBase>()
                .LifestylePerWebRequest());
            
            // CQRS
            container.Register(
                Classes.FromAssemblyNamed("readShorts.Services")
                .BasedOn(typeof(IQueryHandler<,>))
                .WithService.AllInterfaces());

            container.Register(
                Classes.FromAssemblyNamed("readShorts.Services")
                .BasedOn(typeof(ICommandHandler<>))
                .WithService.AllInterfaces());      

        }
    }
}
