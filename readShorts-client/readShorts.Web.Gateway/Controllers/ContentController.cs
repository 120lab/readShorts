using Newtonsoft.Json.Linq;
using readShorts.Web;
using readShorts.Web.Controllers;
using readShorts.Web.Filters;
using readShorts.Web.Networking;
using System;
using System.Net.Http;
using System.Web.Http;

namespace readShorts.Web.Controllers
{
    [RoutePrefix("api/ContentController")]
    //[APIAuthorize]
    //[APIAuthorizeAttribute]
    [APIExceptionAttribute]
    public class ContentController : BaseController
    {
        public ContentController() : base(API_TYPE.Public)
        {
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri]string controller, [FromUri]string param = "")
        {
            var data = networkHttpClient.ReadContentAsync("api/" + controller + "?" + param);

            return data;
        }
    }
}