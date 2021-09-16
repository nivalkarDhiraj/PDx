using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCA.API.LabTests.Model
{
    public enum Gender
    {
        None,
        Male,
        Female,
        Other
    }

    public class Patient
    {
        /// <summary>
        /// Unique identifier for the patient
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient Name
        /// </summary>       
        [Required]
        public string PatientName { get; set; }

        /// <summary>
        /// Date of birth of patient
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gender of patient (0 - None, 1 - Male, 2 - Female, 3 - Other)
        /// </summary>
        [Required]
        public Gender PatientGender { get; set; }

        /// <summary>
        /// Patient email id
        /// </summary>
        [Required]
        public string EmailId { get; set; }

        /// <summary>
        /// Patient contact number
        /// </summary>
        [Required]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Patient address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// To mark for soft delete
        /// </summary>
        public bool isDeleted { get; internal set; }
    }
}
