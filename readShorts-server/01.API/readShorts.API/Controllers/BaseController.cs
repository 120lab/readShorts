using Castle.Windsor;
using System;
using System.Web.Http;

namespace readShorts.API.Internal.Controllers
{
    using Filters;
    using Framework.Core.Interfaces.Log;
    using Framework.Core.Interfaces.Utils;
    using Framework.Core.Log;
    using readShorts.API.Internal;
    using Services;

    [HandleController]
    //[AsyncAuditFilterAttribute]
    public class BaseController : ApiController
    {
        [NonSerialized]
        protected readonly IConfigurationService configurationService;

        [NonSerialized]
        protected readonly IWindsorContainer container;

        [NonSerialized]
        protected readonly ILoggerService loggerService;

        public BaseController(IConfigurationService configurationService = null, ILoggerService loggerService = null)
        {
            this.configurationService = configurationService ?? (configurationService = new ConfigurationService());
            this.container = WebApiApplication.Container;
            this.loggerService = loggerService ?? (loggerService = new Log4NetLoggerService());
        }
    }
}