using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCA.PatientDigital.Cache
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        //private readonly int CACHE_MINUTES = 60; // 60 Minutes
        private readonly IMemoryCache _cache;
        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
            //CACHE_MINUTES = expiryinminutes;
        }
        public T GetFromCache<T>(string key) where T : class
        {
            _cache.TryGetValue(key, out T cachedResponse);
            return cachedResponse as T;
        }
        //public void SetCache<T>(string key, T value) where T : class
        //{
        //    SetCache(key, value, DateTimeOffset.Now.AddMinutes(CACHE_MINUTES));
        //}
        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _cache.Set(key, value, duration);
        }
        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _cache.Set(key, value, options);
        }
        public void ClearCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
