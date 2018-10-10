namespace Framework.Core.Interfaces.Networking
{
    public interface INetworkHttpClient
    {
        T ReadAsync<T>(string route) where T : class;

        //Task<HttpResponseMessage> PostJsonAsync<T>(T fileData, string route) where T : class;
        // Task<bool> PostJsonAsync<T>(T data, string route)
        //    where T : class;


        TK PostJson<T, TK>(T data, string route)
            where T : class
            where TK : class;
    }
}