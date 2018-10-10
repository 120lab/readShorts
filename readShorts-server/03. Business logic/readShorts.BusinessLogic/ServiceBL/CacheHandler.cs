using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Runtime.Caching;

namespace readShorts.BusinessLogic.ServiceBL
{
    public class CacheHandler
    {
        public enum ObjectPolicy
        {
            LookupTables = 0,
            RefreshedTables = 1
        }

        private static CacheHandler instance;
        private CacheHandler() { }

        public static CacheHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheHandler();
                }
                return instance;
            }
        }

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
        internal void AddToCache(JObject data, ObjectPolicy objectPolicy)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = GetCacheItemPolicy(objectPolicy);
            cache.Add(data.ToString(), data, policy);
        }

        internal void AddToCache(string key, JObject data, ObjectPolicy objectPolicy)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = GetCacheItemPolicy(objectPolicy);
            cache.Add(key, data, policy);
        }

        private CacheItemPolicy GetCacheItemPolicy(ObjectPolicy objectPolicy)
        {
            switch (objectPolicy)
            {
                case ObjectPolicy.LookupTables:

                    return new CacheItemPolicy {  AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) };

                case ObjectPolicy.RefreshedTables:

                    return new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(3) }; 
                default:

                    return null;

            }
        }
    }
}
