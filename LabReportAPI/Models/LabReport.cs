using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LabReportAPI.Models
{
    /// <summary>
    /// Class for Lab report details schema creation.
    /// </summary>
    [Table("hca_diag_report_dtls")]
    public class LabReport
    {
        [Key]
        public Int64 diag_test_id { get; set; }
        public Int64 visit_id { get; set; }            //report_id = visit_id
        public string diag_type_name { get; set; }      //Based on the visit id, listed diagnosis should be populated for this report id. 
        public DateTime diag_sample_dtm { get; set; }
        public string diag_unit_messured { get; set; }   //Calculated by the lab tech and feed from UI.
        public string diag_result { get; set; }          //Results are supposed to be derived based on diagnosis limits vs unit mesures, as this API is a middleware it will be processes what is recieved as an input.
        public DateTime diag_result_time { get; set; }
    }
}
