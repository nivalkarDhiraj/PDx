using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabReportAPI.Models
{

    /// <summary>
    /// Class for MD visit diaganosis schema creation.
    /// </summary>
    [Table("hca_md_visit_details")]
    public class MdVisit
    {
        [Key]
        public Int64 visit_id { get; set; }

        [ForeignKey("is_meme_matched")]
        public Int64 meme_ssn { get; set; }             //Check for SSN in patient info in DB.
        public Int64 provider_id { get; set; }            //Provider Name/MailId details should be loaded from Provider module, member visit details alone captured here.
        public DateTime visit_dtm { get; set; }         //Visit date-time should be with-in member eligibility, as this is a middleware no validation will be performed.
        public string md_diag_suggest { get; set; }     //List of diagnosis info will chose by teh MD, As per DB design diagnosis info should be loaded from C/W with allowed limits.
        public string md_note { get; set; }

    }
}
