using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCA.API.LabTests.Model
{
    public class LabReport
    {
        /// <summary>
        /// Unique identifier for lab report
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient id
        /// </summary>
        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Test id
        /// </summary>
        [Required]
        public int LabTestId { get; set; }

        /// <summary>
        /// Sample received on
        /// </summary>
        [Required]
        public DateTime SampleReceivedOn { get; set; }

        /// <summary>
        /// Sample tested on
        /// </summary>
        [Required]
        public DateTime SampleTestedOn { get; set; }

        /// <summary>
        /// Report cretated on
        /// </summary>
        [Required]
        public DateTime ReportCreatedOn { get; set; }

        /// <summary>
        /// Value of test result
        /// </summary>
        public double TestResult { get; set; }

        /// <summary>
        /// Refferred by physician / hospital
        /// </summary>
        public string RefferredBy { get; set; }

        /// <summary>
        /// Recommeded for consultation
        /// </summary>
        public bool NeedConsultation { get; internal set; }

        /// <summary>
        /// To mark for soft delete
        /// </summary>
        public bool isDeleted { get; internal set; }

        /// <summary>
        /// Patient details
        /// </summary>
        public Patient Patient { get; internal set; }

        /// <summary>
        /// Test details
        /// </summary>
        public LabTest LabTest { get; internal set; }

    }
}
