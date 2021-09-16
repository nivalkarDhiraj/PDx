using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Data;
using HCA.API.LabTests.Model;
using Microsoft.EntityFrameworkCore;

namespace HCA.API.LabTests.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly LabReportContext _context;
        public PatientRepository(LabReportContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<Patient> Create(Patient patient)
        {
            patient.isDeleted = false; //active
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Delete patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var patientToDelete = await _context.Patients.FindAsync(id);
            patientToDelete.isDeleted = true; //soft deleted

            _context.Entry(patientToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Patient>> Get()
        {
            var patients = await _context.Patients.ToListAsync();

            if (!patients.Any())
            {
                //Create sample data for testing
                PatientData._context = _context;
                PatientData.CreateSamplePatients();
                                
                patients = await _context.Patients.ToListAsync(); //list for sample data
            }

            return patients.Where(x => !x.isDeleted).ToList(); //list active only
        }

        /// <summary>
        /// Get specific patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Patient> Get(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient;
        }

        /// <summary>
        /// Restore deleted patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Restore(int id)
        {
            var patientToRestore = await _context.Patients.FindAsync(id);
            patientToRestore.isDeleted = false; //active

            _context.Entry(patientToRestore).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<bool> Update(Patient patient)
        {
            var existingPatient = await _context.Patients.FindAsync(patient.Id);
            if (existingPatient == null || existingPatient.isDeleted) //check for active
                return false;

            mapPatient(existingPatient, patient); //map current instance with context instance
            existingPatient.isDeleted = false;//active

            _context.Entry(existingPatient).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Map patient with patient in current context
        /// </summary>
        /// <param name="existingPatient"></param>
        /// <param name="modifiedPatient"></param>
        private void mapPatient(Patient existingPatient, Patient modifiedPatient)
        {
            existingPatient.DateOfBirth = modifiedPatient.DateOfBirth;
            existingPatient.PatientGender = modifiedPatient.PatientGender;
            existingPatient.PatientName = modifiedPatient.PatientName;
            existingPatient.Address = modifiedPatient.Address;
            existingPatient.ContactNumber = modifiedPatient.ContactNumber;
            existingPatient.EmailId = modifiedPatient.EmailId;
        }
    }
}
