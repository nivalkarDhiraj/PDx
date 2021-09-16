using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HCA.API.LabTests.Model
{
    public class LabReportContext : DbContext
    {
        public LabReportContext(DbContextOptions<LabReportContext> options)
            : base(options)
        {

        }

        public DbSet<LabTest> LabTests { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<LabReport> LabReports { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Patient>().Property(e => e.PatientGender)
        //        .HasConversion(g => g.ToString(), g => (Gender)Enum.Parse(typeof(Gender), g));
        //}
    }
}
