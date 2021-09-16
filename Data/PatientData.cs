using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Data
{
    internal class PatientData
    {
        internal static LabReportContext _context { get; set; }
        internal static async void CreateSamplePatients()
        {
            var patient1 = new Model.Patient
            {
                PatientName = "Test Patient 1",
                DateOfBirth = Convert.ToDateTime("1980-05-25"),
                PatientGender = Gender.Male,
                ContactNumber = "(+91) 98235xxxxx",
                EmailId = "testpatient1@gmail.com",
                Address = "Pimpri, Pune, Maharashtra, India - 411018",
                isDeleted = false
            };
            _context.Patients.Add(patient1);

            var patient2 = new Model.Patient
            {
                PatientName = "Test Patient 2",
                DateOfBirth = Convert.ToDateTime("1984-10-10"),
                PatientGender = Gender.Female,
                ContactNumber = "(+91) 98236xxxxx",
                EmailId = "testpatient2@gmail.com",
                Address = "Hadapsar, Pune, Maharashtra, India - 411028",
                isDeleted = false
            };
            _context.Patients.Add(patient2);

            var patient3 = new Model.Patient
            {
                PatientName = "Test Patient 3",
                DateOfBirth = Convert.ToDateTime("2008-09-16"),
                PatientGender = Gender.Male,
                ContactNumber = "(+91) 98237xxxxx",
                EmailId = "testpatient3@gmail.com",
                Address = "Wagholi, Pune, Maharashtra, India - 411038",
                isDeleted = false
            };
            _context.Patients.Add(patient3);

            var patient4 = new Model.Patient
            {
                PatientName = "Test Patient 4",
                DateOfBirth = Convert.ToDateTime("2009-12-14"),
                PatientGender = Gender.Female,
                ContactNumber = "(+91) 98238xxxxx",
                EmailId = "testpatient4@gmail.com",
                Address = "Katraj, Pune, Maharashtra, India - 411048",
                isDeleted = false
            };
            _context.Patients.Add(patient4);

            await _context.SaveChangesAsync();
        }
    }
}
