using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HCA.PatientDigital.BL
{
    public class LabTestManager : ILabTest
    {
        private const string CACHE_KEY = "LabTestList";
        private const int CACHE_EXPIRE = 60;
        private readonly IMemoryCacheProvider _cache;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;

        public List<LabTest> Create(List<LabTest> labTests)
        {
            var labTestList = _cache.GetFromCache<List<LabTest>>(CACHE_KEY);
            // check for empty cache 
            if (labTests == null)
            {
                labTestList = new List<LabTest>();
            }
            if (labTests != null)
            {
                labTestList.AddRange(labTests);
                _cache.SetCache(CACHE_KEY, labTestList, cacheEntryOptions);
            }
            return labTests;
        }
        public List<LabTest> Get()
        {
           
            var labTests = _cache.GetFromCache<List<LabTest>>(CACHE_KEY);
            return labTests;
        }

       

       
    }
}
