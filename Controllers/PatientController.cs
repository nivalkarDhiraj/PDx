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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientRepository.Get();
        }

        /// <summary>
        /// Get sepcific patient by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _patientRepository.Get(id);
            if (patient == null || patient.isDeleted)
                return NotFound();

            return patient;
        }

        /// <summary>
        /// Create patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Patient>> Create([FromBody] Patient patient)
        {
            var newPatient = await _patientRepository.Create(patient);
            return CreatedAtAction(nameof(Create), new { id = newPatient.Id }, newPatient);
        }

        /// <summary>
        /// Update patient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            if (await _patientRepository.Update(patient))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Soft delete patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var patient = await _patientRepository.Get(id);
            if (patient == null || patient.isDeleted)
                return NotFound();

            await _patientRepository.Delete(patient.Id);
            return NoContent();
        }

        /// <summary>
        /// Restore soft deleted patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Restore/{id}")]
        public async Task<ActionResult> Restore(int id)
        {
            var patient = await _patientRepository.Get(id);
            if (patient == null)
                return NotFound();

            await _patientRepository.Restore(patient.Id);
            return NoContent();
        }
    }
}
