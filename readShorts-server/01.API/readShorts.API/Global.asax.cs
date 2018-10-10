using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Framework.Core.Interfaces.Log;
using Framework.Core.Log;
using readShorts.API.Internal.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace readShorts.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;
        protected internal static IWindsorContainer Container { get { return _container; } }
        protected ILogConfig logConfig = new Log4NetConfig();

        protected void Application_Start()
        {
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            logConfig.Start();
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            
            _container.Install(FromAssembly.This());
            _container.Install(FromAssembly.Named("readShorts.Services"));
            _container.Install(FromAssembly.Named("readShorts.BusinessLogic"));
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));

            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;
        }
        protected void Application_End()
        {
            _container.Dispose();
            base.Dispose();
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
