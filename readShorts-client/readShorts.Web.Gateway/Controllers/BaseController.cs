using readShorts.Web.Networking;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace readShorts.Web.Controllers
{
    //[EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    //[APICacheAttribute(50,50,false)]
    public class BaseController : ApiController
    {
        protected INetworkHttpClient networkHttpClient;
        protected CacheHandler cacheHandler = new CacheHandler();

        public BaseController(API_TYPE type)
        {
            networkHttpClient = new NetworkHttpClient(type);
        }

        //[HttpGet]
        //public IHttpActionResult Get([FromUri]string method, [FromUri]string param = "")
        //{
        //    //var data = MemoryCacheHelper.GetCachedData<JObject>("Roles", 20, ExecuteQuery(method, param));

        //    //var data = cacheHandler.GetFromCache(method + param);

        //    //if (data == null)
        //    //{
        //    //    data = networkHttpClient.ReadAsync("api/Lookup?query.tableName=" + method + param);
        //    //    cacheHandler.AddToCache(data);
        //    //}

        //    //return Ok(data);
        //    try
        //    {
        //        var data = networkHttpClient.ReadAsync("api/" + method + param);
        //        return Ok(data);
        //    }
        //    catch (HttpException ex)
        //    {
        //        throw GetResponseExecption(ex);
        //    }
        //}

        protected HttpResponseException GetResponseExecption(HttpException ex)
        {
            HttpResponseMessage resp = new HttpResponseMessage();
            resp.StatusCode = (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), ex.GetHttpCode());
            resp.Content = new StringContent(ex.Message);
            throw new HttpResponseException(resp);
        }
    }

    public class Settings
    {
        public Settings()
        {
        }

        public string Version { get; set; }
        public string UrlSrc { get; set; }
    }
}