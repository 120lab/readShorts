using Newtonsoft.Json.Linq;
using readShorts.Web.Filters;
using readShorts.Web.Networking;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace readShorts.Web.Controllers
{
    [RoutePrefix("api/Invoker")]
    //[APIAuthorize]
    [APIAuthorizeAttribute]
    [APIExceptionAttribute]
    public class InvokerController : BaseController
    {
        public InvokerController() : base(API_TYPE.Public)
        {
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]string controller, [FromUri]string param = "")
        {
            
            string path = "api/" + controller + "?" + param;

            if (controller.ToLower() == "lookup")
            {
                var cacheData = cacheHandler.GetFromCache(path);
                if (cacheData != null)
                {
                    return Ok(cacheData);
                }
            }

            var data = networkHttpClient.ReadAsync(path);

            if (controller.ToLower() == "lookup" && data != null)
            {
                cacheHandler.AddToCache(path, data);
            }

            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromUri]string method, [FromBody]JObject value)
        {
            try
            {
                var data = networkHttpClient.PostJson<JObject>(value, method);
                return Ok(data);
            }
            catch (HttpException ex)
            {
                throw GetResponseExecption(ex);
            }
        }

        // PUT api/<controller>
        [HttpPut]
        public IHttpActionResult Put([FromUri]string method, [FromBody]JObject value)
        {
            try
            {
                var data = networkHttpClient.PutJson<JObject>(value, method);
                return Ok(data);
            }
            catch (HttpException ex)
            {
                throw GetResponseExecption(ex);
            }
        }
    }
}