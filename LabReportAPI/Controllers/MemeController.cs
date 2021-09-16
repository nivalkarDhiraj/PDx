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
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        /// <summary>
        /// Variable creation to use across the class
        /// </summary>
        private PatientDbContext MemeDbContext;
        private CacheHandler MemeCacheHandler = new CacheHandler();
        private IMemoryCache IMemeCache;


        /// <summary>
        /// Constructor for Db context object access
        /// </summary>
        /// <param name="ParamMemeDbContext"></param>
        public MemberController(PatientDbContext ParamMemeDbContext, IMemoryCache ParamMemCache)
        {
            try
            {
                MemeDbContext = ParamMemeDbContext;
                IMemeCache = ParamMemCache;
            }
            catch(Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "MemberController");
            }
        }

        /// <summary>
        /// Function to extract all member information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMeme()
        {
            try
            {
                return MemeDbContext.MemberDetails;
            }
            catch ( Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMeme");
                return BadRequest();
            }

        }

        /// <summary>
        /// Function to extract memebr information usign SSN Key
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <returns></returns>
        [HttpGet("{meme_ssn}")]
        public ActionResult<Member> GetMemeBySSN(Int64 meme_ssn)
        {
            try
            {
                //Extract member information from cache and returns
                if (IMemeCache.TryGetValue(meme_ssn, out Member objOutMeme))
                {
                    return objOutMeme;
                }

                //Extract member information from DB and returns when no cache available.
                Member objTemp = MemeDbContext.MemberDetails.Find(meme_ssn);

                MemeCacheHandler.fnAddMemeToCache(objTemp, meme_ssn, ref IMemeCache);

                if (objTemp == null)
                    return NotFound();
                else
                    return objTemp;
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMemeBySSN");
                return BadRequest();
            }
        }

        /// <summary>
        /// Function to extract member information usign subscriber id and relationship code.
        /// </summary>
        /// <param name="sbsb_id"></param>
        /// <param name="meme_rel"></param>
        /// <returns></returns>
        [HttpGet("{sbsb_id},{meme_rel}")]
        public ActionResult<Member> GetMemeBySbSbID(Int64 sbsb_id, string meme_rel)
        {
            try
            {
                Member objTemp = MemeDbContext.MemberDetails.Where(a => a.sbsb_id == sbsb_id).Where(b => b.meme_rel.ToUpper() == meme_rel.ToUpper()).Single();

                if (objTemp == null)
                    return NotFound();
                else
                    return objTemp;
            }
             catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetMemeBySbSbID");
                return BadRequest();
            }
        }

        /// <summary>
        /// Function get all members based on the certain lab report vs date range.
        /// </summary>
        /// <param name="diag_type_name"></param>
        /// <param name="start_dtm"></param>
        /// <param name="end_dtm"></param>
        /// <returns></returns>
        [HttpGet("{diag_type_name},{start_dtm},{end_dtm}/GetLabReportByDate")]
        public ActionResult<IEnumerable<Member>> GetLabReportByDate(string diag_type_name, DateTime start_dtm, DateTime end_dtm)
        {
            try
            {
                IEnumerable<LabReport> objSelectedLabRpt = MemeDbContext.LabReportDetails.Where(a => a.diag_sample_dtm >= start_dtm).Where(b => b.diag_sample_dtm <= end_dtm).Where(c => c.diag_type_name == diag_type_name);
                IEnumerable<MdVisit> objSelectedVisitLabRpt;
                IEnumerable<Member> objSelectedMeme = MemeDbContext.MemberDetails.Where(a => a.meme_ssn == 0);
                foreach (LabReport objTempLabRpt in objSelectedLabRpt)
                {
                    objSelectedVisitLabRpt = MemeDbContext.MdVisitDetails.Where(a => a.visit_id == objTempLabRpt.visit_id);
                    foreach (MdVisit objTempMdVisit in objSelectedVisitLabRpt)
                    {
                        objSelectedMeme = objSelectedMeme.Append<Member>(MemeDbContext.MemberDetails.Where(a => a.meme_ssn == objTempMdVisit.meme_ssn).Single());
                    }
                }

                return Ok(objSelectedMeme);
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "GetLabReportByDate");
                return BadRequest();
            }
        }

        /// <summary>
        /// Function to save member information from the input JSON
        /// </summary>
        /// <param name="ParamMeme"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<Member>> SaveMeme([FromBody] IEnumerable<Member> ParamMeme)
        {
            try
            {
                //Add new member information into DB & correpondinglly to cache
                foreach (Member objTemp in ParamMeme)
                {
                    MemeDbContext.MemberDetails.Add(objTemp);
                    MemeDbContext.SaveChanges();
                    MemeCacheHandler.fnAddMemeToCache(objTemp, objTemp.meme_ssn, ref IMemeCache);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "SaveMeme");
                return BadRequest();
            }
        }

        /// <summary>
        /// Update member information from Input JSON
        /// </summary>
        /// <param name="ParamMeme"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult UpdateMeme([FromBody] Member ParamMeme)
        {
            try
            {
                //Check for matching ssn with provided
                Member objTemp = MemeDbContext.MemberDetails.Find(ParamMeme.meme_ssn);

                if (objTemp == null)
                {
                    //When not found, Check for matching sbsb with provided 
                    objTemp = MemeDbContext.MemberDetails.Where(a => a.sbsb_id == ParamMeme.sbsb_id).Where(b => b.meme_rel.ToUpper() == ParamMeme.meme_rel.ToUpper()).Single();

                    if (objTemp == null)
                        return NotFound();
                }

                //Update Member chagnes to DB
                MemeDbContext.MemberDetails.Remove(objTemp);
                MemeDbContext.MemberDetails.Add(ParamMeme);
                MemeDbContext.SaveChanges();

                //Update member changes to cache
                MemeCacheHandler.fnAddMemeToCache(ParamMeme, objTemp.meme_ssn, ref IMemeCache);

                return Ok();
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "UpdateMeme");
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete member record using SSN
        /// </summary>
        /// <param name="meme_ssn"></param>
        /// <returns></returns>
        [HttpDelete("{meme_ssn}")]
        public ActionResult DeleteMemeBySSN(Int64 meme_ssn)
        {
            try
            {
                //Check for matching member with SSN in DB
                Member objTemp = MemeDbContext.MemberDetails.Find(meme_ssn);

                if (objTemp == null)
                {
                    return NotFound();
                }
                else
                {
                    //Delete member information from DB
                    MemeDbContext.MemberDetails.Remove(objTemp);
                    MemeDbContext.SaveChanges();

                    //Delete member information from cache
                    MemeCacheHandler.fnAddMemeToCache(null, objTemp.meme_ssn, ref IMemeCache);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "DeleteMemeBySSN");
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete member record using subscriber id and member relations code.
        /// </summary>
        /// <param name="sbsb_id"></param>
        /// <param name="meme_rel"></param>
        /// <returns></returns>
        [HttpDelete("{sbsb_id},{meme_rel}")]
        public ActionResult DeleteMemeBySbSbID(Int64 sbsb_id, string meme_rel)
        {
            try
            {
                //Check for matching DB objects if any available.
                Member objTemp = MemeDbContext.MemberDetails.Where(a => a.sbsb_id == sbsb_id).Where(b => b.meme_rel.ToUpper() == meme_rel.ToUpper()).Single();

                if (objTemp == null)
                {
                    return NotFound();
                }
                else
                {
                    //Delete member information from DB
                    MemeDbContext.MemberDetails.Remove(objTemp);
                    MemeDbContext.SaveChanges();

                    //Delete member information from cache
                    MemeCacheHandler.fnAddMemeToCache(objTemp, objTemp.meme_ssn, ref IMemeCache);

                    return Ok();

                }
            }
            catch (Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "DeleteMemeBySbSbID");
                return BadRequest();
            }

        }
      
    }
}
