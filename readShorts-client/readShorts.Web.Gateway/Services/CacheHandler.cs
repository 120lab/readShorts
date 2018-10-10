using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Runtime.Caching;

namespace readShorts.Web
{
    public class CacheHandler
    {

        /// <summary>
        /// Get item from cache
        /// </summary>
        /// <returns></returns>
        internal JObject GetFromCache(string cacheKey)
        {
            ObjectCache cache = MemoryCache.Default;
            var data = cache.Get(cacheKey);
            if (data == null)
                return null;
            else
                return (JObject)data;
        }

        /// <summary>
        /// Check if item exists - if not add to cache
        /// </summary>
        internal void AddToCache(JObject data)
        {
            // TODO: add pbject to cache
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60) };
            cache.Add(data.ToString(), data, policy);
        }

        internal void AddToCache(string key, JObject data)
        {
            // TODO: add pbject to cache
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) };
            cache.Add(key, data, policy);
        }
    }
}
