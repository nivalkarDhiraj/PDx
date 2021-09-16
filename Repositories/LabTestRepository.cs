using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Data;
using HCA.API.LabTests.Model;
using Microsoft.EntityFrameworkCore;

namespace HCA.API.LabTests.Repositories
{
    public class LabTestRepository : ILabTestRepository
    {
        private readonly LabReportContext _context;
        public LabTestRepository(LabReportContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create lab test
        /// </summary>
        /// <param name="labTest"></param>
        /// <returns></returns>
        public async Task<LabTest> Create(LabTest labTest)
        {
            labTest.isDeleted = false; //active
            _context.LabTests.Add(labTest);

            await _context.SaveChangesAsync();
            return labTest;
        }

        /// <summary>
        /// Delete lab test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var labTestToDelete = await _context.LabTests.FindAsync(id);
            labTestToDelete.isDeleted = true; //soft deleted

            _context.Entry(labTestToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all lab tests
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LabTest>> Get()
        {
            var labTests = await _context.LabTests.ToListAsync();

            if (!labTests.Any())
            {
                //create sample reports for testing
                LabTestData._context = _context;
                await LabTestData.CreateSampleTests();

                labTests = await _context.LabTests.ToListAsync(); //list for sample data
            }

            return labTests.Where(x => !x.isDeleted).ToList(); //list active only
        }

        /// <summary>
        /// Get specific lab test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LabTest> Get(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            return labTest;
        }

        /// <summary>
        /// Resore deleted lab test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Restore(int id)
        {
            var labTestToRestore = await _context.LabTests.FindAsync(id);
            labTestToRestore.isDeleted = false; //active

            _context.Entry(labTestToRestore).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update lab test
        /// </summary>
        /// <param name="labTest"></param>
        /// <returns></returns>
        public async Task<bool> Update(LabTest labTest)
        {
            var existingLabTest = await _context.LabTests.FindAsync(labTest.Id);
            if (existingLabTest == null || existingLabTest.isDeleted) //check for active
                return false;

            //map current instance with context instance
            mapLabTest(existingLabTest, labTest);
            existingLabTest.isDeleted = false; //active

            _context.Entry(existingLabTest).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Map lab test with lab test in current context
        /// </summary>
        /// <param name="existingLabTest"></param>
        /// <param name="modifiedLabTest"></param>
        private void mapLabTest(LabTest existingLabTest, LabTest modifiedLabTest)
        {
            existingLabTest.MaxLimit = modifiedLabTest.MaxLimit;
            existingLabTest.MinimumRequiredQty = modifiedLabTest.MinimumRequiredQty;
            existingLabTest.MinLimit = modifiedLabTest.MinLimit;
            existingLabTest.SampleType = modifiedLabTest.SampleType;
            existingLabTest.TestType = modifiedLabTest.TestType;
            existingLabTest.Description = modifiedLabTest.Description;
        }
    }
}
