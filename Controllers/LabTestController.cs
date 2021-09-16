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
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestRepository _labTestRepository;

        public LabTestController(ILabTestRepository labTestRepository)
        {
            _labTestRepository = labTestRepository;
        }

        /// <summary>
        /// Get all tests
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<LabTest>> Get()
        {
            return await _labTestRepository.Get();
        }

        /// <summary>
        /// Get specific test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]

        public async Task<ActionResult<LabTest>> Get(int id)
        {
            var labTest = await _labTestRepository.Get(id);
            if (labTest == null || labTest.isDeleted)
                return NotFound();

            return labTest;
        }

        /// <summary>
        /// Create test
        /// </summary>
        /// <param name="labTest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<LabTest>> Create([FromBody] LabTest labTest)
        {
            var newLabTest = await _labTestRepository.Create(labTest);
            return CreatedAtAction(nameof(Create), new { id = newLabTest.Id }, newLabTest);
        }

        /// <summary>
        /// Update test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labTest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LabTest labTest)
        {
            if (id != labTest.Id)
            {
                return BadRequest();
            }

            if (await _labTestRepository.Update(labTest))
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Soft delete test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var labTest = await _labTestRepository.Get(id);
            if (labTest == null || labTest.isDeleted)
                return NotFound();

            await _labTestRepository.Delete(labTest.Id);
            return NoContent();
        }

        /// <summary>
        /// Restore soft deleted test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Restore/{id}")]
        public async Task<ActionResult> Restore(int id)
        {
            var labTest = await _labTestRepository.Get(id);
            if (labTest == null)
                return NotFound();

            await _labTestRepository.Restore(labTest.Id);
            return NoContent();
        }
    }
}
