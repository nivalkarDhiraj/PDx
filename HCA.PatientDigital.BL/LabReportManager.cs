using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace HCA.PatientDigital.BL
{
    public class LabReportManager : ILabReportManager
    {
        private const string CACHE_KEY = "LabReportList";
        private readonly int CACHE_EXPIRE = 0;
        private readonly IMemoryCacheProvider _cache;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private IConfiguration _config;
        
        public LabReportManager(IMemoryCacheProvider memoryCache, IConfiguration configuration)
        {
            // to read configuration
            _config = configuration;
            var expiryinminutes = Convert.ToInt32(_config["PortalCacheConfiguration:expiryinminutes"]);
            CACHE_EXPIRE = expiryinminutes <= 0 ? 60 : expiryinminutes;
            _cache = memoryCache;                     
            cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(CACHE_EXPIRE));
        }        
        // create patient lab report
        public LabReport Create(LabReport labReport)
        {
           
            var labReports = _cache.GetFromCache<List<LabReport>>(CACHE_KEY);
            // check for empty cache 
            if(labReports == null)
            {
                labReports = new List<LabReport>();
            }
            labReports.Add(labReport);
            _cache.SetCache(CACHE_KEY, labReports, cacheEntryOptions);
            return labReport;
        }
        // update patient
        public LabReport Update(LabReport patient)
        {
            var labReports = _cache.GetFromCache<List<LabReport>>(CACHE_KEY);
            // check for empty cache
            if (labReports != null && labReports.Count>0)
            {
                // check and remove the item.
                var labReportData = labReports.Where(x => x.ReportId.ToLower() == patient.ReportId.ToLower()).SingleOrDefault();
                if(labReportData != null)
                {
                    labReports.Remove(labReportData);
                }
                // add new item to collection
                labReports.Add(patient);
                _cache.SetCache(CACHE_KEY, labReports, cacheEntryOptions);
            }
            return patient;
        }
        // delete patient
        public LabReport Delete(string labReportId)
        {
            var labReports = _cache.GetFromCache<List<LabReport>>(CACHE_KEY);
            LabReport labReportData = null;
            // check for empty cache
            if (labReports != null)
            {
                // check and remove the item.
                labReportData = labReports.Where(x => x.ReportId.ToLower() == labReportId.ToLower()).SingleOrDefault();
                if (labReportData != null)
                {
                    labReports.Remove(labReportData);
                    _cache.SetCache(CACHE_KEY, labReports, cacheEntryOptions);
                }               
            }
            return labReportData;
        }
        // get patient list
        public List<LabReport> Get()
        {
            var labReports = _cache.GetFromCache<List<LabReport>>(CACHE_KEY);            
            return labReports;
        }
        // get patient by patientcode
        public LabReport Get(string labReportId)
        {
           

            var labReports = _cache.GetFromCache<List<LabReport>>(CACHE_KEY);
            LabReport labReport = null;
            // check for empty cache
            if (labReports == null)
            {
                // check and remove the item.
                labReport = labReports.Where(x => x.ReportId.ToLower() == labReportId.ToLower()).SingleOrDefault();                
            }
            return labReport;
        }

       
    }
}
