using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Web;
using System.Web.Http;

namespace readShorts.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            //System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            //TelemetryConfiguration.Active.InstrumentationKey = RoleEnvironment.GetConfigurationSettingValue("APPINSIGHTS_INSTRUMENTATIONKEY");
            // TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (Request.HttpMethod == "OPTIONS")
            //{
            //    string httpOrigin = Request.Params["HTTP_ORIGIN"];
            //    if (httpOrigin == null) httpOrigin = "*";
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", httpOrigin);
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "accept, content-type, customername, modulename, serviceaction, servicename");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            //    HttpContext.Current.Response.StatusCode = 200;
            //    var httpApplication = sender as HttpApplication;
            //    httpApplication.CompleteRequest();
            //}
        }
    }
}
