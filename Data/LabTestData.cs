using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Data
{
    internal class LabTestData
    {
        internal static LabReportContext _context { get; set; }

        internal async static Task CreateSampleTests()
        {
            var LabTest1 = new Model.LabTest
            {
                Description = "Blood Count",
                SampleType = SampleTypes.BloodSample,
                TestType = TestTypes.ChemicalTest,
                MinimumRequiredQty = 50,
                MinLimit = 100,
                MaxLimit = 1000,
                isDeleted = false
            };
            _context.LabTests.Add(LabTest1);

            var LabTest2 = new Model.LabTest
            {
                Description = "Glucose Tolerance",
                SampleType = SampleTypes.BloodSample,
                TestType = TestTypes.ChemicalTest,
                MinimumRequiredQty = 100,
                MinLimit = 200,
                MaxLimit = 2000,
                isDeleted = false
            };
            _context.LabTests.Add(LabTest2);

            var LabTest3 = new Model.LabTest
            {
                Description = "Kidney Function",
                SampleType = SampleTypes.UrineSample,
                TestType = TestTypes.ChemicalTest,
                MinimumRequiredQty = 150,
                MinLimit = 300,
                MaxLimit = 3000,
                isDeleted = false
            };
            _context.LabTests.Add(LabTest3);

            var LabTest4 = new Model.LabTest
            {
                Description = "Brain Scanning",
                SampleType = SampleTypes.None,
                TestType = TestTypes.PhysicalTest,
                MinimumRequiredQty = 0,
                MinLimit = 0,
                MaxLimit = 0,
                isDeleted = false
            };
            _context.LabTests.Add(LabTest4);

            await _context.SaveChangesAsync();
        }
    }
}
