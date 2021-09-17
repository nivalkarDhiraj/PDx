using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HCA.PatientDigital.BL
{
    public class PatientManager : IPatientManager
    {
        private const string CACHE_KEY = "PatientList";
        private const int CACHE_EXPIRE = 60;
        private readonly IMemoryCacheProvider _cache;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private readonly ILabReportManager _labReportManager; 
        public PatientManager(IMemoryCacheProvider memoryCache, ILabReportManager labReportManager)
        {
            _cache = memoryCache;
            cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(CACHE_EXPIRE));
            _labReportManager = labReportManager;
        }
        // create patient
        public Patient Create(Patient patient)
        {
            //TODO: duplicate check           
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            // check for empty cache
            if(patients==null)
            {
                patients = new List<Patient>();                
            }            
            patients.Add(patient);
            _cache.SetCache(CACHE_KEY, patients, cacheEntryOptions);
            return patient;
        }
        // update patient
        public Patient Update(Patient patient)
        {
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            // check for empty cache
            if (patients != null)
            {
                // check and remove the item.
                var patientRecord = patients.Where(x => x.PatientId.ToLower() == patient.PatientId.ToLower()).SingleOrDefault();
                if(patientRecord != null)
                {
                    patients.Remove(patientRecord);
                }
                // add new item to collection
                patients.Add(patient);
                _cache.SetCache(CACHE_KEY, patients, cacheEntryOptions);
            }
            return patient;
        }
        // delete patient
        public Patient Delete(string patientId)
        {
            Patient patientRecord = null;
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            // check for empty cache
            if (patients != null)
            {
                // check and remove the item.
                patientRecord = patients.Where(x => x.PatientId.ToLower() == patientId.ToLower()).SingleOrDefault();
                if (patientRecord != null)
                {
                    patients.Remove(patientRecord);
                }                
                _cache.SetCache(CACHE_KEY, patients, cacheEntryOptions);
            }
            return patientRecord;
        }
        // get patient list
        public List<Patient> Get()
        {
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            return patients;
        }
        // get patient by patientcode
        public Patient Get(string patientId)
        {
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            Patient patientRecord = null;
            // check for empty cache
            if (patients != null)
            {
                // check and remove the item.
               patientRecord = patients.Where(x => x.PatientId.ToLower() == patientId.ToLower()).SingleOrDefault();
            }
            return patientRecord;
        }

        public List<Patient> Get(string testName, string startDate, string endDate)
        {
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            List<LabReport> labReportList = null;

            // filters
            if (patients!=null)
            {
                // get lab report 
                labReportList = _labReportManager.Get();
                if(labReportList != null)
                {
                   // filter test name
                    if (!string.IsNullOrEmpty(testName))
                    {
                        labReportList = labReportList.Where(repo => 
                        repo.LabTests.Any(test => test.TestName.ToUpperInvariant() == testName.ToUpperInvariant())).ToList();
                    }
                    // filter start date 
                    if (!string.IsNullOrEmpty(startDate))
                    {
                        labReportList = labReportList.Where(repo =>
                        repo.ReportTime >= Convert.ToDateTime(startDate)).ToList();
                    }
                    // filter end date 
                    if (!string.IsNullOrEmpty(endDate))
                    {
                        labReportList = labReportList.Where(repo =>
                        repo.ReportTime >= Convert.ToDateTime(endDate)).ToList();
                    }
                }
                // filter patients
                if (labReportList != null)
                {
                    // List of patients
                    var patientList = labReportList.Select(p => p.PatientId).ToList();
                    patients = patients.Where(pat => patientList.Contains(pat.PatientId)).ToList();
                }

            }
            return patients;
        }
        public bool isExists(Patient patient)
        {
            bool isExists = false;

            //TODO: duplicate check           
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            // check for empty cache
            if (patients != null)
            {
                var duplicatePatient = patients.Where(pa => pa.Name.ToLower() == patient.Name.ToLower()
                && pa.Dob == patient.Dob).FirstOrDefault();
                if (duplicatePatient != null)
                {
                    isExists = true;
                }
            }
            return isExists;
        }
        public bool isExists(string patientId)
        {
            bool isExists = false;

            //TODO: duplicate check           
            var patients = _cache.GetFromCache<List<Patient>>(CACHE_KEY);
            // check for empty cache
            if (patients != null)
            {
                var duplicatePatient = patients.Where(pa => pa.PatientId.ToLower() == patientId.ToLower()).FirstOrDefault();
                if (duplicatePatient != null)
                {
                    isExists = true;
                }
            }
            return isExists;
        }

    }
}
