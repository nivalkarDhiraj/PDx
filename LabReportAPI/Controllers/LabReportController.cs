using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabReportAPI;
using LabReportAPI.Models;

namespace LabReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabReportController : ControllerBase
    {

        private PatientDbContext LabReportDbContext;
        private CacheHandler LabReportCacheHandler = new CacheHandler();
        private IMemoryCache ILabReportCache;

        public LabReportController(PatientDbContext ParamLabReportDbContext, IMemoryCache ParamLabReportCache)
        {
            try
            {
                LabReportDbContext = ParamLabReportDbContext;
                ILabReportCache = ParamLabReportCache;
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "MemberController");
            }
        }

        /// <summary>
        /// Function to extract all lab report information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<LabReport>> GetLabReport()
        {
            try
            {
                return LabReportDbContext.LabReportDetails;
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetLabReport");
                return BadRequest();
            }

        }

        /// <summary>
        /// Function to extract lab report information usign report id
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <returns></returns>
        [HttpGet("{diag_test_id}")]
        public ActionResult<LabReport> GetLabReportByID(Int64 diag_test_id)
        {
            try
            {
                //Extract member information from cache and returns
                if (ILabReportCache.TryGetValue(diag_test_id, out LabReport objOutReport))
                {
                    return objOutReport;
                }

                LabReport objSelectedLabRpt = LabReportDbContext.LabReportDetails.Find(diag_test_id);
                if (objSelectedLabRpt != null)
                {

                    //Update member changes to cache
                    LabReportCacheHandler.fnAddLabReportToCache(objSelectedLabRpt, diag_test_id, ref ILabReportCache);

                    return Ok(objSelectedLabRpt);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetLabReportByID");
                return BadRequest();
            }
        }

        /// <summary>
        /// Function to extract lab report information usign report id
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <returns></returns>
        [HttpGet("{visit_id},{diag_test_id}")]
        public ActionResult<IEnumerable<LabReport>> GetLabReportByVisit(Int64 visit_id, Int64 diag_test_id)
        {
            try
            {
                IEnumerable<LabReport> objSelectedLabRpt;

                if(diag_test_id == 0)
                    objSelectedLabRpt = LabReportDbContext.LabReportDetails.Where(a => a.visit_id == visit_id);
                else
                    objSelectedLabRpt = LabReportDbContext.LabReportDetails.Where(a => a.visit_id == visit_id).Where(b => b.diag_test_id == diag_test_id);

                if (objSelectedLabRpt != null)
                {
                    foreach (LabReport objTemp in objSelectedLabRpt)
                    {
                        LabReportCacheHandler.fnAddLabReportToCache(objTemp, objTemp.diag_test_id, ref ILabReportCache);
                    }

                    return Ok(objSelectedLabRpt);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetLabReportByVisit");
                return BadRequest();
            }
        }

        [HttpGet("{diag_type_name},{start_dtm},{end_dtm}/GetLabReportByDate")]
        public ActionResult<IEnumerable<LabReport>> GetLabReportByDiagType(string diag_type_name, DateTime start_dtm, DateTime end_dtm)
        {
            try
            {
                IEnumerable<LabReport> objSelectedLabRpt = LabReportDbContext.LabReportDetails.Where(a => a.diag_sample_dtm >= start_dtm).Where(b => b.diag_sample_dtm <= end_dtm).Where(c => c.diag_type_name == diag_type_name);

                if (objSelectedLabRpt != null)
                {
                    foreach (LabReport objTemp in objSelectedLabRpt)
                    {
                        LabReportCacheHandler.fnAddLabReportToCache(objTemp, objTemp.diag_test_id, ref ILabReportCache);
                    }

                    return Ok(objSelectedLabRpt);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetLabReportByDiagType");
                return BadRequest();
            }
        }


        /// <summary>
        /// Function to save lab report information from the input JSON
        /// </summary>
        /// <param name="objMeme"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<LabReport>> SaveLabReport([FromBody] IEnumerable<LabReport> ParamLabReport)
        {
            try
            {
                //Add new member information into DB & correpondinglly to cache
                foreach(LabReport objTemp in ParamLabReport)
                {
                    LabReportDbContext.LabReportDetails.Add(objTemp);
                    LabReportDbContext.SaveChanges();
                    LabReportCacheHandler.fnAddLabReportToCache(objTemp, objTemp.diag_test_id, ref ILabReportCache);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "SaveLabReport");
                return BadRequest();
            }
        }

        /// <summary>
        /// Update lab report information from Input JSON
        /// </summary>
        /// <param name="objMeme"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult UpdateLabReport([FromBody] IEnumerable<LabReport> ParamLabReport)
        {
            try
            {
                //Find existing record requires update and do DB changes.
                //If not found existing match the insert new lab report.
                foreach (LabReport objTemp in ParamLabReport)
                {
                    LabReport objOldLabRpt = LabReportDbContext.LabReportDetails.Where(a => a.diag_test_id == objTemp.diag_test_id).Where(b => b.visit_id == objTemp.visit_id).Single();
                    //Update Member chagnes to DB
                    if(objOldLabRpt != null)
                        LabReportDbContext.LabReportDetails.Remove(objOldLabRpt);

                    LabReportDbContext.LabReportDetails.Add(objTemp);
                    LabReportDbContext.SaveChanges();

                    //Update member changes to cache
                    LabReportCacheHandler.fnAddLabReportToCache(objTemp, objOldLabRpt.diag_test_id, ref ILabReportCache);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "UpdateLabReport");
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete lab report details record using SSN
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <returns></returns>
        [HttpDelete("{diag_test_id}")]
        public ActionResult DeleteLabReportByID(Int64 diag_test_id)
        {
            try
            {
                //Check for matching member with SSN in DB

                LabReport objTemp = LabReportDbContext.LabReportDetails.Find(diag_test_id);

                if (objTemp == null)
                {
                    return NotFound();
                }
                else
                {
                    //Delete member information from DB
                    LabReportDbContext.LabReportDetails.Remove(objTemp);
                    LabReportDbContext.SaveChanges();

                    //Delete member information from cache
                    LabReportCacheHandler.fnAddLabReportToCache(null, objTemp.diag_test_id, ref ILabReportCache);

                return Ok();
                }
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "DeleteLabReportByID");
                return BadRequest();
            }
        }

        /// <summary>
        /// Extract Lab Report based on the date ranges provided.
        /// </summary>
        /// <param name="DateRange_Start"></param>
        /// <param name="DateRange_End"></param>
        /// <returns></returns>



    }
}
