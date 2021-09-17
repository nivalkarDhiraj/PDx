using System;
using System.Collections.Generic;
using System.Text;
using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;

namespace HCA.PatientDigital.BL
{
    public interface IPatientManager
    {
        Patient Create(Patient patient);
        Patient Update(Patient patient);
        Patient Delete(string patientId);
        List<Patient> Get();
        Patient Get(string patientId);
        List<Patient> Get(string testName, string startDate, string endDate);
        bool isExists(Patient patient);
        bool isExists(string patientId);
    }
}
