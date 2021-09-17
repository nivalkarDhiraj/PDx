using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HCA.PlatformDigital.Entity
{
    public class Patient
    {
        public string PatientId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Dob is required.")]
        public string Dob { get; set; }
        [Required]
        public bool IsMale { get; set; }     
        public string  ContactNo { get; set; }
        
    }
}
