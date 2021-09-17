﻿//using Microsoft.Extensions.Caching.Distributed;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Threading.Tasks;

//namespace HCA.PlatformDigital.Common
//{
//    public class DistributedCacheExtensions : IDistributedCacheExtensions
//    {
//        public IDistributedCache _cache;
//        public BinaryFormatter binaryFormatter;

//        public DistributedCacheExtensions(IDistributedCache cache)
//        {
//            this.binaryFormatter = new BinaryFormatter();
//            this._cache = cache;
//        }

//        public async Task<T> GetCacheAsync<T>(string key) where T : class
//        {
//            try
//            {
//                byte[] values = await _cache.GetAsync(key);

//                if (values == null) return null;

//                BinaryFormatter binaryFormatter = new BinaryFormatter();
//                using (MemoryStream memoryStream = new MemoryStream(values))
//                {
//                    return binaryFormatter.Deserialize(memoryStream) as T;
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task SetCacheAsync<T>(T values, string key)
//        {
//            try
//            {
//                MemoryStream mStream = new MemoryStream();

//                binaryFormatter.Serialize(mStream, values);

//                mStream.ToArray();

//                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
//                {
//                    AbsoluteExpiration = DateTime.Now.AddDays(7),
//                    SlidingExpiration = TimeSpan.FromSeconds(30)
//                };

//                await _cache.SetAsync(key, mStream.ToArray());

//                mStream.Close();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public async Task RemoveCacheAsync(string key)
//        {
//            try
//            {
//                await _cache.RemoveAsync(key);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public T GetCache<T>(string key) where T : class
//        {
//            try
//            {
//                byte[] values = _cache.Get(key);

//                if (values == null) return null;

//                BinaryFormatter binaryFormatter = new BinaryFormatter();
//                using (MemoryStream memoryStream = new MemoryStream(values))
//                {
//                    return binaryFormatter.Deserialize(memoryStream) as T;
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public void SetCache<T>(T values, string key)
//        {
//            try
//            {
//                MemoryStream mStream = new MemoryStream();

//                binaryFormatter.Serialize(mStream, values);

//                mStream.ToArray();

//                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
//                {
//                    AbsoluteExpiration = DateTime.Now.AddDays(7),
//                    SlidingExpiration = TimeSpan.FromSeconds(30)
//                };

//                _cache.Set(key, mStream.ToArray());

//                mStream.Close();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public void RemoveCache(string key)
//        {
//            try
//            {
//                _cache.Remove(key);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}
