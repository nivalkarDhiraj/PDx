using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;
using HCA.API.LabTests.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCA.API.LabTests.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LabReportController : ControllerBase
    {
        private readonly ILabReportRepository _labReportRepository;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IPatientRepository _patientRepository;

        public LabReportController(ILabReportRepository labReportRepository, 
            ILabTestRepository labTestRepository, IPatientRepository patientRepository)
        {
            _labReportRepository = labReportRepository;
            _labTestRepository = labTestRepository;
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Get all reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<LabReport>> Get()
        {
            return await _labReportRepository.Get();
        }

        /// <summary>
        /// Get particular report by id
        /// </summary>
        /// <param name="id">Report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<LabReport>> Get(int id)
        {
            var labReport = await _labReportRepository.Get(id);
            if (labReport == null || labReport.isDeleted)
                return NotFound();

            return labReport;
        }

        /// <summary>
        /// Get report for a test based on report created date
        /// </summary>
        /// <param name="labTestId">LabTest Id</param>
        /// <param name="fromDt">Start Date</param>
        /// <param name="toDt">End Date</param>
        /// <returns></returns>
        [HttpGet("GetByLabTest/{labTestId}/{fromDt}/{toDt}")]
        public async Task<IEnumerable<object>> GetByLabTest(int labTestId, DateTime fromDt, DateTime toDt)
        {
            return await _labReportRepository.Get(labTestId, fromDt, toDt);
        }

        /// <summary>
        /// Create report
        /// </summary>
        /// <param name="labReport"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<LabReport>> Create([FromBody] LabReport labReport)
        {
            var newLabReport = await _labReportRepository.Create(labReport);

            if (newLabReport == null)
                return BadRequest();

            return CreatedAtAction(nameof(Create), new { id = newLabReport.Id }, newLabReport);
        }

        /// <summary>
        /// Update report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labReport"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LabReport labReport)
        {
            if (id != labReport.Id)
            {
                return BadRequest();
            }

            if (await _labReportRepository.Update(labReport))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Soft delete report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var labReport = await _labReportRepository.Get(id);
            if (labReport == null || labReport.isDeleted)
                return NotFound();

            await _labReportRepository.Delete(labReport.Id);
            return NoContent();
        }

        /// <summary>
        /// Restore soft deleted report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Restore/{id}")]
        public async Task<ActionResult> Restore(int id)
        {
            var labReport = await _labReportRepository.Get(id);
            if (labReport == null)
                return NotFound();

            await _labReportRepository.Restore(labReport.Id);
            return NoContent();
        }
    }
}
