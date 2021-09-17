using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.PlatformDigital.Entity
{
    public class LabReport
    {
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportTime { get; set; }
        public string PatientId { get; set; }
        public List<LabTest> LabTests { get; set; }
    }
}
