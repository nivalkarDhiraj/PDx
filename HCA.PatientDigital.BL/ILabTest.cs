using System;
using System.Collections.Generic;
using System.Text;
using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;

namespace HCA.PatientDigital.BL
{
    public interface ILabTest
    {
        List<LabTest> Create(List<LabTest> labTests);        
        List<LabTest> Get();
        

    }
}
