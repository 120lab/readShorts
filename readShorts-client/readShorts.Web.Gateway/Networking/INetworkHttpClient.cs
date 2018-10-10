using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace readShorts.Web.Networking
{
    public interface INetworkHttpClient
    {
        JObject ReadAsync(string route);

        //Task<HttpResponseMessage> PostJsonAsync<T>(T fileData, string route) where T : class;
        // Task<bool> PostJsonAsync<T>(T data, string route)
        //    where T : class;
        HttpResponseMessage ReadContentAsync(string route);

        JObject PostJson<T>(T data, string route)
            where T : class;

        JObject PutJson<T>(T data, string route)
            where T : class;
    }
}