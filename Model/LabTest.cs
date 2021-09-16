using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HCA.API.LabTests.Model
{
    public enum TestTypes
    {
        None,
        ChemicalTest,
        PhysicalTest
    }

    public enum SampleTypes
    {
        None,
        BloodSample,
        UrineSample,
        StoolSample,
        SwabSample
    }

    public class LabTest
    {
        /// <summary>
        /// Unique identifier for test
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of test (0 - Chemical, 1 - Physical)
        /// </summary>
        [Required]
        public TestTypes TestType { get; set; }

        /// <summary>
        /// Description of the test
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Type of sample for the test (0 - None, 1 - Blood, 2 - Urine, 3 - Stool, 4 - Swab, 5 - Other)
        /// </summary>
        [Required]
        public SampleTypes SampleType { get; set; }

        /// <summary>
        /// Minimum amount of quantity of sample needed for test
        /// </summary>
        public Single MinimumRequiredQty { get; set; }

        /// <summary>
        /// Minimum limit of value for the result
        /// </summary>
        public double MinLimit { get; set; }

        /// <summary>
        /// Maximum limit of value for the result
        /// </summary>
        public double MaxLimit { get; set; }

        /// <summary>
        /// To mark for soft delete
        /// </summary>
        public bool isDeleted { get; internal set; }
    }
}
