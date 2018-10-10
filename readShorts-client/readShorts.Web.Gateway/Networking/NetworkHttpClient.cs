using Newtonsoft.Json.Linq;
using readShorts.Web.Services;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace readShorts.Web.Networking
{
    public enum API_TYPE
    {
        Public,
        External
    }

    public class NetworkHttpClient : INetworkHttpClient
    {
        private ConfigurationService configurationService = new ConfigurationService();
        public static readonly string GeneralError = "שגיאה. נא לפנות למנהל המערכת.";
        private readonly string url;
        private readonly TimeSpan TimeOut = TimeSpan.FromSeconds(600);
        private HttpClient client;

        public NetworkHttpClient(API_TYPE type, string token = "")
        {
            WebRequestHandler handler = new WebRequestHandler();
            //X509Certificate2 cer = CertStoreHandler(type);
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.UseProxy = true;
            //handler.ClientCertificates.Add(cer);
            client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Add("ClientCert", Convert.ToBase64String(cer.RawData));/
            client.DefaultRequestHeaders.Add("ClientCert", Convert.ToBase64String(new byte[] { 0x20, 0x20 }));
            switch (type)
            {
                case API_TYPE.Public:
                    url = configurationService.InnerHostAddress();
                    break;

                case API_TYPE.External:
                    url = configurationService.innerHostAddressAuth();
                    var authToken = string.IsNullOrEmpty(token) ? HttpContext.Current.Session["SessionID"] : token;
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + authToken);
                    break;

                default:
                    break;
            }
            client.BaseAddress = new Uri(url);
            client.Timeout = TimeOut;
        }

        private X509Certificate2 CertStoreHandler(API_TYPE type)
        {
            X509Store store = null;
            store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            string thumbprint = "";
            switch (type)
            {
                case API_TYPE.Public:
                    thumbprint = configurationService.GetThumbprint();
                    break;

                case API_TYPE.External:
                    thumbprint = configurationService.GetThumbprintAuth();
                    break;
            }
            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            var cer = certs[0];
            store.Close();
            return cer;
        }

        public JObject ReadAsync(string route)
        {
            //client.DefaultRequestHeaders.Add("Authorization", "Basic " + HttpContext.Current.Session.SessionID);
            HttpResponseMessage response = client.GetAsync(route).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<JObject>().Result;

                return res;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)response.StatusCode, msg);
            }
        }

        public HttpResponseMessage ReadContentAsync(string route)
        {
            //client.DefaultRequestHeaders.Add("Authorization", "Basic " + HttpContext.Current.Session.SessionID);
            HttpResponseMessage response = client.GetAsync(route).Result;
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)response.StatusCode, msg);
            }
        }

        public JObject PostJson<T>(T data, string route)
            where T : class
        {
            var task = client.PostAsJsonAsync<T>(new Uri(string.Format("{0}{1}", url, "api/" + route)), data);

            // 1. GETTING RESPONSE - NOT ASYNC WAY
            task.Wait(); //THIS WILL HOLD THE THREAD AND IT WON'T BE ASYNC ANYMORE!
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<JObject>().Result;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)response.StatusCode, msg);
            }
        }

        public JObject PutJson<T>(T data, string route)
            where T : class
        {
            var task = client.PutAsJsonAsync<T>(new Uri(string.Format("{0}{1}", url, "api/" + route)), data);

            // 1. GETTING RESPONSE - NOT ASYNC WAY
            task.Wait(); //THIS WILL HOLD THE THREAD AND IT WON'T BE ASYNC ANYMORE!
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<JObject>().Result;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)response.StatusCode, msg);
            }
        }

        private JObject CreateErrorReponse()
        {
            JObject response = new JObject();
            response.Add("ErrorMessages", new JArray(new JObject(new JProperty("LogLevel", 0), new JProperty("Message", GeneralError))));
            return response;
        }
    }
}