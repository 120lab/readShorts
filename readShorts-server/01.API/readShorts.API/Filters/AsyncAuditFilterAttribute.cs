using Castle.Windsor;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace readShorts.API.Filters
{
    public class AsyncAuditFilterAttribute : ActionFilterAttribute 
    {
        public ICommandDispatcher CommandDispatcher { get; private set; }

        public AsyncAuditFilterAttribute()
        {            
        }

        public AsyncAuditFilterAttribute(IWindsorContainer container, ICommandDispatcher commandDispatcher)
        {
            this.CommandDispatcher = commandDispatcher;
        }

        ///public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            HttpRequestMessage message = actionExecutedContext.Request;
            string rawCredentials = null;

            // TODO: move to client-side API!!!!!!!
            message.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "IdentityNumber", "123456789"))));

            if (message.Headers.Authorization != null)
            {
                rawCredentials = message.Headers.Authorization.Parameter;
            }

            CreateAuditCommand command = new CreateAuditCommand()
            {
                ActionName = actionExecutedContext.Request.RequestUri.ToString(),
                UserId = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(rawCredentials)).Split(':')[1]
            };

            this.CommandDispatcher = readShorts.API.WebApiApplication.Container.Resolve<ICommandDispatcher>();            
            this.CommandDispatcher.Dispatch<CreateAuditCommand, AuditViewModel>(command);
           
        }
    }
}