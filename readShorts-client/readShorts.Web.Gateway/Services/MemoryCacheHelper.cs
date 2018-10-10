using System;
using System.Runtime.Caching;
using Newtonsoft.Json.Linq;

namespace readShorts.Web
{
    public static class MemoryCacheHelper
    {
        internal static T GetCachedData<T>(string key, int cacheTimePolicyMinutes, Func<T> action) 
            where T : class
        {

            Lazy<T> lazyObject = new Lazy<T>(() => action());
            
            CacheItemPolicy cip = new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(cacheTimePolicyMinutes))
            };

            var returnedLazyObject = MemoryCache.Default.AddOrGetExisting(key, lazyObject, cip);
            return ((Lazy<T>)returnedLazyObject).Value;
        }
    }
}
