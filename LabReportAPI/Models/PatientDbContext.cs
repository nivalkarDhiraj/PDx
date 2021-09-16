using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LabReportAPI.Models
{
    public class PatientDbContext : DbContext
    {

        /// <summary>
        /// Function for Sql-lite DB context creation
        /// </summary>
        /// <param name="options"></param>
        public PatientDbContext(DbContextOptions<PatientDbContext> options) 
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Object creation for member info table
        /// </summary>
        public DbSet<Member> MemberDetails { get; set; }
        public DbSet<MdVisit> MdVisitDetails { get; set; }
        public DbSet<LabReport> LabReportDetails { get; set; }

    }
}
