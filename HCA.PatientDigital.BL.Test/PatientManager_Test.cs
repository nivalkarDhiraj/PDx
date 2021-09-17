using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HCA.PatientDigital.BL.Test
{
    public class PatientManager_Test
    {
        //TODO: Need to complete the testing for this component
        // TODO: Need to have some negative test as well.
        List<Patient> patients = null;
        
        [SetUp]
        public void Setup()
        {
            //patients = PopulatePatient();
            //Mock<IMemoryCache> _memCache = new Mock<IMemoryCache>();
            //Mock<IMemoryCacheProvider> _memoryCache = new Mock<IMemoryCacheProvider>();
        }

        [Test]
        public void Create_Patient()
        {
            Mock<IPatientManager> _patientManager = new Mock<IPatientManager>();
            Patient patient = new Patient();
            patient.Name = "Viranjay Singh";
            patient.Dob = DateTime.Now.AddYears(-40).ToString();
            patient.IsMale = true;
            var result = _patientManager.Setup(p => p.Create(patient)).Returns(patient);            
            Assert.IsTrue(result!=null);
        }
        [Test]
        public void Create_Update()
        {
            Mock<IPatientManager> _patientManager = new Mock<IPatientManager>();
            Patient patient = new Patient();
            patient.Name = "Viranjay Singh";
            patient.Dob = DateTime.Now.AddYears(-40).ToString();
            patient.IsMale = true;
            var result = _patientManager.Setup(p => p.Update(patient)).Returns(patient);
            Assert.IsTrue(result != null);
        }
        [Test]
        public void Create_Delete()
        {
            Mock<IPatientManager> _patientManager = new Mock<IPatientManager>();
            string patinetId = "0f7eacb9-d4c2-47dd-80a1-b5dba26d7123";
            Patient patient = new Patient();
            patient.Name = "Viranjay Singh";
            patient.Dob = DateTime.Now.AddYears(-40).ToString();
            patient.IsMale = true;
            var result = _patientManager.Setup(p => p.Delete(patinetId)).Returns(patient);
            Assert.IsTrue(result != null);
        }
        [Test]
        public void Create_Get()
        {
            Mock<IPatientManager> _patientManager = new Mock<IPatientManager>();
            var result = _patientManager.Setup(p => p.Get()).Returns(patients);
            Assert.IsTrue(result != null);
        }
        [Test]
        public void Create_GetByPatient_Id()
        {
            Mock<IPatientManager> _patientManager = new Mock<IPatientManager>();
            string patinetId = "0f7eacb9-d4c2-47dd-80a1-b5dba26d7123";
            Patient patient = new Patient();
            patient.Name = "Viranjay Singh";
            patient.Dob = DateTime.Now.AddYears(-40).ToString();
            patient.IsMale = true;
            var result = _patientManager.Setup(p => p.Get(patinetId)).Returns(patient);
            Assert.IsTrue(result != null);
        }

        // TODO: Need ti have some negative test as well.

        #region populate data for test
        private List<Patient> PopulatePatient()
        {
           patients = new List<Patient>() {
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7123", 
                Name = "James ", Dob = DateTime.Now.AddYears(-32).ToString(), IsMale  =true},
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7124",
                Name = "Patricia", Dob = DateTime.Now.AddYears(-35).ToString(), IsMale = false },
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7125",
                Name = "Michael", Dob = DateTime.Now.AddYears(-38).ToString(), IsMale = true },
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7125",
                Name = "William", Dob = DateTime.Now.AddYears(-52).ToString(), IsMale = true },
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7126",
                Name = "David", Dob = DateTime.Now.AddYears(-55).ToString(), IsMale = true },
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d7127",
                Name = "Richard", Dob = DateTime.Now.AddYears(-56).ToString(), IsMale = true },
            new Patient() { PatientId="0f7eacb9-d4c2-47dd-80a1-b5dba26d718",
                Name = "Linda", Dob = DateTime.Now.AddYears(-57).ToString(), IsMale = false }           
            };
            return patients;
        }
        #endregion
    }
}