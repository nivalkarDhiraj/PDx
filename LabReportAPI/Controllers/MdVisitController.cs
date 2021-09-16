using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabReportAPI;
using LabReportAPI.Models;

namespace LabReportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MDVisitController : ControllerBase
    {

        private PatientDbContext MdVisitDbContext;
        private CacheHandler MdVisitCacheHandler = new CacheHandler();
        private IMemoryCache IMdCache;

        /// <summary>
        /// Constructor for Db context object access
        /// </summary>
        /// <param name="objParam"></param>
        public MDVisitController(PatientDbContext ParamMdVisitDbContext, IMemoryCache ParamMdCache)
        {
            try
            {
                MdVisitDbContext = ParamMdVisitDbContext;
                IMdCache = ParamMdCache;
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "MemberController");
            }
        }

        /// <summary>
        /// Get Md visit details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MdVisit>> GetMdVist()
        {
            try
            {
                return MdVisitDbContext.MdVisitDetails;
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMdVist");
                return BadRequest();
            }

        }

        /// <summary>
        /// Get MD visiting details based on visit id.
        /// </summary>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        [HttpGet("{visit_id}")]
        public ActionResult<MdVisit> GetMDVisitByVisitID(Int64 visit_id)
        {
            try
            {
                if (IMdCache.TryGetValue(visit_id, out MdVisit objOutTemp))
                {
                    return objOutTemp;
                }

                //Check for matching visit id in DB when not available in cache.
                MdVisit objTemp = MdVisitDbContext.MdVisitDetails.Find(visit_id);

                if (objTemp == null)
                    return NotFound();
                else
                    return objTemp;

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMDVisitByVisitID");
                return BadRequest();
            }
        }
        /// <summary>
        /// Get memebr visiting information based on meme_ssn
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <param name="visit_dtm"></param>
        /// <returns></returns>
        [HttpGet("{meme_ssn},{visit_dtm}")]
        public ActionResult<MdVisit> GetMDVisitByMemeSSN(Int64 meme_ssn, DateTime visit_dtm)
        {
            try
            {
                //Check for matching ssn with provided
                MdVisit objTemp = MdVisitDbContext.MdVisitDetails.Where(a => a.meme_ssn == meme_ssn).Where(b => b.visit_dtm == visit_dtm).Single();

                if (objTemp == null)
                    return NotFound();
                else
                    return objTemp;

            }
            catch(Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMDVisitByMemeSSN");
                return BadRequest();
            }
        }

        /// <summary>
        /// Save MD visit details
        /// </summary>
        /// <param name="ParamMdVisit"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MdVisit> SaveMdVisit([FromBody] MdVisit ParamMdVisit)
        {
            try
            {
                //Add new MD Visit information into DB & correpondinglly to cache
                MdVisitDbContext.MdVisitDetails.Add(ParamMdVisit);
                MdVisitDbContext.SaveChanges();
                MdVisitCacheHandler.fnAddMDVisitToCache(ParamMdVisit, ParamMdVisit.visit_id, ref IMdCache);

                return Ok();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "SaveMdVisit");
                return BadRequest();
            }
        }

        /// <summary>
        /// Update md visit information from Input JSON
        /// </summary>
        /// <param name="objMDVisit"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult UpdateMDVisit([FromBody] MdVisit ParamMdVisit)
        {
            try
            {
                //Check for matching visit information with provided
                MdVisit objTemp = MdVisitDbContext.MdVisitDetails.Find(ParamMdVisit.visit_id);

                //Update visit md chagnes to DB
                MdVisitDbContext.MdVisitDetails.Remove(objTemp);
                MdVisitDbContext.MdVisitDetails.Add(ParamMdVisit);
                MdVisitDbContext.SaveChanges();

                //Update MD Visiting information changes to cache
                MdVisitCacheHandler.fnAddMDVisitToCache(ParamMdVisit, ParamMdVisit.visit_id, ref IMdCache);

                return Ok();
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "UpdateMDVisit");
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete md visit record using visit id
        /// </summary>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        [HttpDelete("{visit_id}")]
        public ActionResult DeleteMDVisitBySSN(Int64 visit_id)
        {
            try
            {
                //Check for matching md visit with visit_id in DB
                MdVisit objTemp = MdVisitDbContext.MdVisitDetails.Find(visit_id);

                if (objTemp == null)
                {
                    return NotFound();
                }
                else
                {
                    //Delete md visit information from DB
                    MdVisitDbContext.MdVisitDetails.Remove(objTemp);
                    MdVisitDbContext.SaveChanges();

                    //Delete MD Visiting information from cache
                    MdVisitCacheHandler.fnAddMDVisitToCache(null, visit_id, ref IMdCache);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "DeleteMDVisitBySSN");
                return BadRequest();
            }
        }

    }
}
