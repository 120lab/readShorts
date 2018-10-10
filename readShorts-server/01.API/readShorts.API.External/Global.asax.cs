using System.Web;
using System.Web.Http;
using Framework.Core.Interfaces.Log;

namespace readShorts.API.External
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected ILogConfig logConfig;// = new Log4NetConfig();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Common.WebApiApplication.ConfigureWindsor(GlobalConfiguration.Configuration, "readShorts.API.External");

            logConfig = Common.WebApiApplication.Container.Resolve<ILogConfig>();
            logConfig.Start();
        }
                
        protected void Application_End()
        {
            base.Dispose();
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
