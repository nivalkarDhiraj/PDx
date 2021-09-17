using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCA.PlatformDigital.Common
{
    public static class Message
    {
        public const string BAD_REQUEST = "Please check input value and data type.";
        public const string INVALID_PATIENT_DETAILS = "Invalid Patient Details.";
        public const string INVALID_LABREPORT_DETAIL = "Invalid LabReport Details.";
        public const string INVALID_PATINET_ID= "Patient not valid.";
        public const string PATIENT_EXISTS = "Patient details already exists.";
        public const string INTERNAL_SERVER_ERROR= "Internal Server Error.";        
    }
}
