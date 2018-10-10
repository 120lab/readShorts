using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace readShorts.Web.Filters
{
    public class APIExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            System.Diagnostics.Trace.TraceError("\"" + actionExecutedContext.Exception.ToString() + "\"");
            if (!(actionExecutedContext.Exception is HttpException
                && ((HttpException)actionExecutedContext.Exception).ErrorCode != 500))
                actionExecutedContext.Exception = new Exception("General Error Has Occured");
            base.OnException(actionExecutedContext);
        }

        //public override System.Threading.Tasks.Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, System.Threading.CancellationToken cancellationToken)
        //{
        //    System.Diagnostics.Trace.TraceError(actionExecutedContext.Exception.Message + "\n" + actionExecutedContext.Exception.StackTrace);
        //    return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        //}
    }
}