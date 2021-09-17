using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.PlatformDigital.Entity
{
    public class LabTest
    {
        public string TestName { get; set; }
        public DateTime TestDateTime { get; set; }
        public List<LabTestResult> Tests { get; set; }
        public string Description { get; set; }
    }    
}
