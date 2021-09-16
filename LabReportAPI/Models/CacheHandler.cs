using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using LabReportAPI;

namespace LabReportAPI.Models
{
    public class CacheHandler
    {

        /// <summary>
        /// Function to handle cache object to Add/Delete/Update cache when not expired.
        /// </summary>
        /// <param name="AddMemeToCache"></param>
        /// <param name="strMemeSSN"></param>
        public void fnAddMemeToCache(Member AddMemeToCache, Int64 strMemeSSN, ref IMemoryCache IMemeCache)
        {
            try
            {
                //Remove if any existing unexpired member object matching SSN
                if(IMemeCache != null)
                {
                    if (IMemeCache.TryGetValue(strMemeSSN, out Member objOutMeme))
                    {
                        IMemeCache.Remove(strMemeSSN);
                    }
                }

                //Set cache entry options
                if (AddMemeToCache != null)
                {
                    MemoryCacheEntryOptions objCacheExpire = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };

                    //Set Meme information into cache
                    IMemeCache.Set(AddMemeToCache.meme_ssn, AddMemeToCache, objCacheExpire);
                }

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "Program Start - Main");
            }
        }

        /// <summary>
        /// Function to handle cache object to Add/Delete/Update cache when not expired.
        /// </summary>
        /// <param name="objAddMeme"></param>
        /// <param name="strMemeSSN"></param>
        public void fnAddMDVisitToCache(MdVisit AddMDVisitToCache, Int64 visit_id, ref IMemoryCache IMdVisitCache)
        {
            try
            {
                //Remove if any existing unexpired member object matching SSN
                if (IMdVisitCache != null)
                {
                    if (IMdVisitCache.TryGetValue(visit_id, out MdVisit objOutMDVisit))
                    {
                        IMdVisitCache.Remove(visit_id);
                    }
                }

                //Set cache entry options
                if (AddMDVisitToCache != null)
                {
                    MemoryCacheEntryOptions objCacheExpire = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };

                    //Set Meme information into cache
                    IMdVisitCache.Set(visit_id, AddMDVisitToCache, objCacheExpire);
                }

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "Program Start - Main");
            }
        }

        /// <summary>
        /// Function to handle cache object to Add/Delete/Update cache when not expired.
        /// </summary>
        /// <param name="objAddMeme"></param>
        /// <param name="strMemeSSN"></param>
        public void fnAddLabReportToCache(LabReport AddLabReportToCache, Int64 diag_test_id, ref IMemoryCache IMdVisitCache)
        {
            try
            {
                //Remove if any existing unexpired member object matching SSN
                if (IMdVisitCache != null)
                {
                    if (IMdVisitCache.TryGetValue(diag_test_id, out LabReport objOutMeme))
                    {
                        IMdVisitCache.Remove(diag_test_id);
                    }
                }

                //Set cache entry options
                if (AddLabReportToCache != null)
                {
                    MemoryCacheEntryOptions objCacheExpire = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(Startup.AbsExpiryAddMin)),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(Convert.ToInt32(Startup.SlidingExpiryAddMin))
                    };

                    //Set Meme information into cache
                    IMdVisitCache.Set(diag_test_id, AddLabReportToCache, objCacheExpire);
                }

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "Program Start - Main");
            }
        }

    }
}
