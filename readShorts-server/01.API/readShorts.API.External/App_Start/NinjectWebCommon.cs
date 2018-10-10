[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(readShorts.API.External.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(readShorts.API.External.App_Start.NinjectWebCommon), "Stop")]

namespace readShorts.API.External.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Framework.Core.Interfaces.CQRS;
    using Framework.Core.CQRS;
    using System.Web.Hosting;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind(x =>
            //x.FromAssembliesMatching("Framework.Core.dll", "Framework.Core.Interfaces.dll", "readShorts.API.External.dll")
            // .SelectAllClasses()
            // .BindAllInterfaces()
            //);

            kernel.Bind(convention => convention
                .FromAssembliesInPath(HostingEnvironment.ApplicationPhysicalPath + @"bin\")
                .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
                //.Where(t => t.Name.StartsWith(useFakeData ? "Fake" : "Real"))
                .BindAllInterfaces());

            kernel.Bind(convention => convention
                .FromAssembliesInPath(HostingEnvironment.ApplicationPhysicalPath + @"bin\")
                .SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>))
                //.Where(t => t.Name.StartsWith(useFakeData ? "Fake" : "Real"))
                .BindDefaultInterfaces());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("*")
            //    .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
            //    .BindAllInterfaces());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("*")
            //    .SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>))
            //    .BindAllInterfaces());

            kernel.Rebind<ICommandDispatcher>()
                 .To<CommandDispatcher>()
                 .InRequestScope();

            kernel.Rebind<IQueryDispatcher>()
                 .To<QueryDispatcher>()
                 .InRequestScope();




            //kernel.Bind(x => x
            //    .FromAssembliesMatching("Framework.Core.dll", "Framework.Core.Interfaces.dll", "readShorts.API.External.dll")
            //    .SelectAllClasses()
            //    .BindDefaultInterface());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("Framework.Core.dll", "Framework.Core.Interfaces.dll")
            //    .SelectAllClasses()
            //    .BindDefaultInterface());

            ////kernel.Bind(x => x
            ////    .FromAssembliesMatching("AT.SampleApp.Cqrs.dll", "MongoRepository.dll")
            ////    .SelectAllClasses().InheritedFrom(typeof(IRepository<>))
            ////    .BindAllInterfaces());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("Framework.Core.dll")
            //    .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
            //    .BindAllInterfaces());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("*")
            //    .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
            //    .BindAllInterfaces());

            //kernel.Bind(x => x
            //    .FromAssembliesMatching("*")
            //    .SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>))
            //    .BindAllInterfaces());

        }
    }
}
