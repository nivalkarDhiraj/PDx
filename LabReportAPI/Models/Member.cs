using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabReportAPI.Models
{

    /// <summary>
    /// Class for member table schema creation.
    /// </summary>
    [Table("hca_meme_member")]
    public class Member
    {
        public Int64 sbsb_id { get; set; }
        public string meme_rel { get; set; }
        public string meme_first_name { get; set; }
        public string meme_last_name { get; set; }
        public DateTime meme_dbo { get; set; }
        public string meme_gender { get; set; }
        [Key]
        public Int64 meme_ssn { get; set; }
        public string meme_mail_id { get; set; }

    }

}
