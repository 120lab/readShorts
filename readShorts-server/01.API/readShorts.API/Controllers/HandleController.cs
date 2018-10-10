using PostSharp.Aspects;
using System;

namespace readShorts.API.Internal
{

    [Serializable]
    public class HandleController : OnMethodBoundaryAspect
    {

        public override void CompileTimeInitialize(System.Reflection.MethodBase method, AspectInfo aspectInfo)
        {
            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //var request = System.Web.HttpContext.Current.Request; // request.URL to get url
            //ApiController controller = (ApiController)args.Instance;
            //var context = controller.ControllerContext;

            //var rd = HttpContext.Current.Request.RequestContext.RouteData;
            //var x = (rd.Values.Values.First() as IHttpRouteData[]).First();
            //var m = (x.Route.DataTokens.First().Value as HttpActionDescriptor[]).First();
            //var f = m.GetType().GetProperty("MethodInfo").GetValue(m) as System.Reflection.MethodInfo;
            // f.DeclaringType.Name
            // f.Name
            // f.ReturnType.Name

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            base.OnExit(args);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            base.OnSuccess(args);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            base.OnException(args);
        }
    }
}