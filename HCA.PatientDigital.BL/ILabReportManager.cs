using System;
using System.Collections.Generic;
using System.Text;
using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;

namespace HCA.PatientDigital.BL
{
    public interface ILabReportManager
    {
        LabReport Create(LabReport labReport);
        LabReport Update(LabReport labReport);
        LabReport Delete(string labReportId);
        List<LabReport> Get();
        LabReport Get(string labReportId);

    }
}
