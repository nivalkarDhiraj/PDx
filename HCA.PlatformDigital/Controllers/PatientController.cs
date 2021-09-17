using HCA.PlatformDigital.Common;
using HCA.PlatformDigital.Entity;
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
using System.Globalization;

namespace HCA.PlatformDigital.Controllers
{
    [Route("patient")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        //private List<Patient> patients = null;
        private readonly ILog _logger;
        private readonly IConfiguration _config;        
        private readonly IPatientManager _patientManager;

        public PatientController(IConfiguration configuration, IPatientManager patientManager)
        {
            //_cache = cache;
            // logging
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            // config 
            _config = configuration;           
            // Business Logic layer
            _patientManager = patientManager;
        }

        /// <summary>
        /// Creates a patient.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// {
        ///  "patientId": "",
        ///  "name": "James John",
        ///  "dob": "1972-10-15",
        ///  "isMale": true,
        ///  "contactNo": ""
        ///}
        ///
        /// </remarks>
        /// <response code="200">Patient Added Successful</response>
        /// <response code="400">Invalid Patient Details</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(Patient patient)
        {
            try
            {
                // validate mandate fields and data type
                if (patient == null || string.IsNullOrEmpty(patient.Name) || patient.Dob == null)
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATIENT_DETAILS));
                }
                // check for valid dob
                if (!string.IsNullOrEmpty(patient.Dob))
                {
                    if (DateTime.TryParse(patient.Dob, out DateTime Temp) == false)
                    {
                        return await Task.FromResult(StatusCode(400, Message.INVALID_PATIENT_DETAILS));
                    }
                }
                // check for duplicate patient
                bool isExists = _patientManager.isExists(patient);
                if (isExists)
                {
                    return await Task.FromResult(StatusCode(409, Message.PATIENT_EXISTS));
                }
                patient.PatientId = Guid.NewGuid().ToString();
                patient = _patientManager.Create(patient);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Update a patient.
        /// </summary>
        /// <remarks>
        /// Sample request:(PatientId is required)
        ///
        /// {
        ///  "patientId": "db88cc9c-f911-4ce2-afef-e229244a93e0",
        ///  "name": "James John",
        ///  "dob": "1975-12-15",
        ///  "isMale": true,
        ///  "contactNo": ""
        ///}
        ///
        /// </remarks>
        /// <response code="200">Patient Updated Successful</response>
        /// <response code="400">Invalid Patient Details</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(Patient patient)
        {
            try
            {
                // validate mandate fields and data type
                if (patient == null || string.IsNullOrEmpty(patient.Name) || string.IsNullOrEmpty(patient.PatientId) || patient.Dob == null)
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATIENT_DETAILS));
                }
                // check for valid dob
                if (!string.IsNullOrEmpty(patient.Dob))
                {
                    if (DateTime.TryParse(patient.Dob, out DateTime Temp) == false)
                    {
                        return await Task.FromResult(StatusCode(400, Message.INVALID_PATIENT_DETAILS));
                    }
                }
                // check for duplicate patient
                bool isExists = _patientManager.isExists(patient.PatientId);
                if (!isExists)
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATINET_ID));
                }
                // update records
                patient = _patientManager.Update(patient);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Delete a patient.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// /patient/delete/{patientId}
        /// (PatientId is required)
        /// PatientId as route parameter.
        /// </remarks>
        /// <response code="200">Patient Deleted Successfull (true).</response>
        /// <response code="200">Patient not valid.</response>
        /// <response code="400">Invalid Patient Details</response>
        /// <response code="401">Unauthorized</response>
        [HttpDelete]
        [Route("delete/{patientId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string patientId)
        {
            try
            {
                // validate mandate fields and data type
                if (string.IsNullOrEmpty(patientId))
                {
                    return await Task.FromResult(StatusCode(400, Message.INVALID_PATIENT_DETAILS));
                }
                var patient = _patientManager.Delete(patientId);
                if (patient == null) return await Task.FromResult(NoContent());
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// List of patients.
        /// </summary>
        /// <remarks>        
        /// /patient/get        
        /// </remarks>
        /// <response code="200">List Of patients.</response>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(List<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public async Task<IActionResult> Get()
        {
            try
            {
                var patients = _patientManager.Get();
                if (patients == null) return await Task.FromResult(NoContent());
                return await Task.FromResult(Ok(patients));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Get patient details by patientid.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// /patient/get/{patientId}                
        /// Pass PatientId as route parameter.
        /// PatientId is required.
        /// </remarks>
        /// <response code="200">Patient details</response>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("get/{patientId}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(string patientId)
        {
            try
            {
                var patient = _patientManager.Get(patientId);
                if (patient == null) return await Task.FromResult(NoContent());
                return await Task.FromResult(Ok(patient));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }
        /// <summary>
        /// Search patient by test name and date range.
        /// </summary>
        /// <remarks>
        /// Sample request:(PatientId is required)
        /// {URL}/patient/get/031af2b1-225a-4e28-af57-0f8e566984d5                
        /// Pass PatientId as route parameter.
        /// </remarks>
        /// <response code="200">List Of patient details</response>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("filter/{testName}/{startDate?}/{endDate?}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        
        public async Task<IActionResult> GetByTestName(string testName, string startDate, string endDate)
        {
            try
            {
                // test name is required
                if (string.IsNullOrEmpty(testName))
                {
                    return await Task.FromResult(StatusCode(400, Message.BAD_REQUEST));
                }
                // start date should be valid datetime.
                if (!string.IsNullOrEmpty(startDate))
                {
                    if (DateTime.TryParse(startDate, out DateTime Temp) == false)
                    {
                        return await Task.FromResult(StatusCode(400, Message.BAD_REQUEST));
                    }
                }
                // start date should be valid datetime.
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (DateTime.TryParse(endDate, out DateTime Temp) == false)
                    {
                        return await Task.FromResult(StatusCode(400, Message.BAD_REQUEST));
                    }
                }

                var patient = _patientManager.Get(testName, startDate, endDate);
                if (patient == null) return await Task.FromResult(NoContent());
                return await Task.FromResult(Ok(patient));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + " " + ex.StackTrace);
                return StatusCode(500, Message.INTERNAL_SERVER_ERROR);
            }
        }

        #region private method

        //private void SetCacheData()
        //{
        //    var patients = PopulatePatient();
        //    var cacheEntryOptions = new MemoryCacheEntryOptions()
        //                       .SetSlidingExpiration(TimeSpan.FromMinutes(60));
        //    _cache.SetCache("PatientList", patients, cacheEntryOptions);
        //}

        //private List<Patient> PopulatePatient()
        //{
        //    List<Patient> patients = new List<Patient>() {
        //    new Patient() { Id = 1, Name = "James ", Dob = DateTime.Now.AddYears(-32), Gender =true},
        //    new Patient() { Id = 2, Name = "Patricia", Dob = DateTime.Now.AddYears(-35), Gender = false },
        //    new Patient() { Id = 3, Name = "Michael", Dob = DateTime.Now.AddYears(-38), Gender = true },
        //    new Patient() { Id = 4, Name = "William", Dob = DateTime.Now.AddYears(-52), Gender = true },
        //    new Patient() { Id = 5, Name = "David", Dob = DateTime.Now.AddYears(-55), Gender = true },
        //    new Patient() { Id = 6, Name = "Richard", Dob = DateTime.Now.AddYears(-56), Gender = true },
        //    new Patient() { Id = 7, Name = "Linda", Dob = DateTime.Now.AddYears(-57), Gender = false },
        //    new Patient() { Id = 8, Name = "Thomas", Dob = DateTime.Now.AddYears(-61), Gender = true },
        //    new Patient() { Id = 9, Name = "Charles", Dob = DateTime.Now.AddYears(-62), Gender = true },
        //    new Patient() { Id = 10, Name = "Sarah", Dob = DateTime.Now.AddYears(-63), Gender = false },
        //    };
        //    return patients;
        //}

        #endregion

    }

}
