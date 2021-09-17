using HCA.PlatformDigital.Common;
using HCA.PlatformDigital.Entity;
using HCA.PatientDigital.Identity;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HCA.PatientDigital.BL;


namespace HCA.PlatformDigital.Controllers
{
    [Route("labreport")]
    [ApiController]
    [Authorize]
    public class LabReportController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IConfiguration _config;
        private readonly ILabReportManager _labReportManager;
        private readonly IPatientManager _patientManager;

        public LabReportController(IConfiguration configuration, IPatientManager patientManager, 
            ILabReportManager labReportManager)
        {
            
            // logging
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            // config 
            _config = configuration;            
            // Business Logic layer
            _labReportManager = labReportManager;
            _patientManager = patientManager;
        }
        /// <summary>
        /// Create a LabReport.
        /// </summary>
        /// <remarks>
        /// PatientId is required and should exists.
        /// 
        /// Sample request:
        ///       
        ///{
        ///  "reportId": "",
        ///  "reportName": "Blood Test",
        ///  "reportTime": "2021-01-01T07:16:59.706Z",
        ///  "patientId": "6d349d02-53b1-407f-bedb-2668b4d988b4",
        ///  "labTests": [
        ///    {
        ///      "testName": "CBC",
        ///      "testDateTime": "2021-09-14T07:16:59.706Z",
        ///      "tests": [
        ///        {
        ///          "name": "WHITE BLOOD CELL COUNT",
        ///          "result": "3.9",
        ///          "resultExpected": "3.8-10.8 Thousand/uL",
        ///          "technology": "PHOTOMETRY",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "RED BLOOD CELL COUNT",
        ///          "result": "5.24",
        ///          "resultExpected": "4.20 - 5.80 Million/uL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC TEST",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "HEMOGLOBIN",
        ///          "result": "16.5",
        ///          "resultExpected": "13.2 - 17.1 g/dL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "MCV",
        ///          "result": "94.9",
        ///          "resultExpected": "80.0 - 100.0 fL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "Param1",
        ///              "value": "This is to describe more test property"
        ///            }
        ///          ]
        ///        }
        ///      ],
        ///      "description": "desc"
        ///    }
        ///  ]
        ///}
        /// </remarks>
        /// <response code="200">LabReport Added Successfully</response>
        /// <response code="400">Invalid Lab Report Details</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(LabReport), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(LabReport labReport)
        {
            try
            {
                // validate mandate fields and data type
                if (labReport== null || string.IsNullOrEmpty(labReport.PatientId)
                    || (labReport.LabTests == null))
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_LABREPORT_DETAIL));
                }                
                // patients should registered before the test
                bool isExists = _patientManager.isExists(labReport.PatientId);
                if (!isExists)
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATINET_ID));
                }
                // TODO: perform other required validations.

                labReport.ReportId = Guid.NewGuid().ToString();
                labReport.ReportTime = DateTime.Now;
                labReport = _labReportManager.Create(labReport);
                return Ok(labReport);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Update a LabReport.
        /// </summary>
        /// <remarks>
        /// The LabReport and Patient should exists (PatientId and reportId is required).
        /// 
        /// Sample request:
        ///       
        ///{
        ///  "reportId": "d7c75b33-d2ef-43ef-9d90-68054a9bfb56",
        ///  "reportName": "Blood Test",
        ///  "reportTime": "2021-01-01T07:16:59.706Z",
        ///  "patientId": "6d349d02-53b1-407f-bedb-2668b4d988b4",
        ///  "labTests": [
        ///    {
        ///      "testName": "CBC",
        ///      "testDateTime": "2021-09-14T07:16:59.706Z",
        ///      "tests": [
        ///        {
        ///          "name": "WHITE BLOOD CELL COUNT",
        ///          "result": "3.9",
        ///          "resultExpected": "3.8-10.8 Thousand/uL",
        ///          "technology": "PHOTOMETRY",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "RED BLOOD CELL COUNT",
        ///          "result": "5.24",
        ///          "resultExpected": "4.20 - 5.80 Million/uL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC TEST",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "HEMOGLOBIN",
        ///          "result": "16.5",
        ///          "resultExpected": "13.2 - 17.1 g/dL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "",
        ///              "value": ""
        ///            }
        ///          ]
        ///        },
        ///        {
        ///          "name": "MCV",
        ///          "result": "94.9",
        ///          "resultExpected": "80.0 - 100.0 fL",
        ///          "technology": "PH",
        ///          "method": "GOD-PAP METHOD",
        ///          "description": "CBC ",
        ///          "testParameters": [
        ///            {
        ///              "name": "Param1",
        ///              "value": "This is to describe more test property"
        ///            }
        ///          ]
        ///        }
        ///      ],
        ///      "description": "desc"
        ///    }
        ///  ]
        ///}
        /// </remarks>
        /// <response code="200">LabReport Added Successfully.</response>
        /// <response code="400">Invalid Lab Report Details.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(LabReport), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(LabReport labReport)
        {
            try
            {
                // validate mandate fields and data type
                if (labReport == null || string.IsNullOrEmpty(labReport.PatientId)
                    || (labReport.LabTests == null) || string.IsNullOrEmpty(labReport.ReportId))
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_LABREPORT_DETAIL));
                }
                // patients should registered before the test
                bool isExists = _patientManager.isExists(labReport.PatientId);
                if (!isExists)
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATINET_ID));
                }                
                labReport.ReportTime = DateTime.Now;
                labReport = _labReportManager.Update(labReport);
                return Ok(labReport);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Delete a Lab Report.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// /labreport/delete/{reportId}
        /// (reportId is required)
        /// 
        /// Provide reportId as route parameter.
        /// </remarks>
        /// <response code="200">LabReport Deleted Successfull (true).</response>
        /// <response code="200">LabReport not valid.</response>
        /// <response code="400">Invalid LabReport Details</response>
        /// <response code="401">Unauthorized</response>        
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        [Route("delete/{reportId}")]
        public async Task<IActionResult> Delete(string reportId)
        {
            try
            {
                // validate required fields, data type & others input data.
                if (string.IsNullOrEmpty(reportId))
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_LABREPORT_DETAIL));
                }
                var labReport = _labReportManager.Delete(reportId);
                if (labReport == null) return await Task.FromResult(NoContent());
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// List of LabReports.
        /// </summary>
        /// <remarks>        
        /// /labreport/get        
        /// </remarks>
        /// <response code="200">List Of LabReports.</response>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(List<LabReport>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var labReports = _labReportManager.Get();
                if (labReports == null) return await Task.FromResult(NoContent());
                return await Task.FromResult(Ok(labReports));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Get lab report by labReportId.
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        /// /labreport/get/{labReportId}
        /// 
        /// Provide labReportId as route parameter.
        ///         
        /// labReportId is required.
        /// </remarks>
        /// <response code="200">LabReport details</response>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("get/{labReportId}")]
        [ProducesResponseType(typeof(LabReport), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]        
        public async Task<IActionResult> GetById(string labReportId)
        {
            try
            {
                var labReport = _labReportManager.Get(labReportId);
                if (labReport == null) return await Task.FromResult(NoContent());
                return await Task.FromResult(Ok(labReport));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
