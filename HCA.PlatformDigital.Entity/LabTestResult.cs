using System;
using System.Collections.Generic;
using System.Text;

namespace HCA.PlatformDigital.Entity
{
    public class LabTestResult
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string ResultExpected { get; set; }
        public string Technology { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public List<TestParameters> TestParameters { get; set; }
    }
    public class TestParameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}