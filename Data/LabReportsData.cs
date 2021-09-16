using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Data
{
    internal class LabReportsData
    {
        internal static LabReportContext _context { get; set; }

        internal static async Task CreateSampleReports()
        {
            bool isValid = false;

            var LabReport1 = new Model.LabReport
            {
                LabTestId = 1,
                PatientId = 1,
                RefferredBy = "Dr. Physician 1",
                SampleReceivedOn = Convert.ToDateTime("2021-01-10"),
                SampleTestedOn = Convert.ToDateTime("2021-01-11"),
                ReportCreatedOn = Convert.ToDateTime("2021-01-12"),
                TestResult = 125,
                NeedConsultation = false,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport1))
            {
                _context.LabReports.Add(LabReport1);
                isValid = true;
            }

            var LabReport2 = new Model.LabReport
            {
                LabTestId = 2,
                PatientId = 3,
                RefferredBy = "Dr. Physician 2",
                SampleReceivedOn = Convert.ToDateTime("2021-02-20"),
                SampleTestedOn = Convert.ToDateTime("2021-02-21"),
                ReportCreatedOn = Convert.ToDateTime("2021-02-22"),
                TestResult = 250,
                NeedConsultation = false,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport2))
            {
                _context.LabReports.Add(LabReport2);
                isValid = true;
            }

            var LabReport3 = new Model.LabReport
            {
                LabTestId = 1,
                PatientId = 2,
                RefferredBy = "Dr. Physician 3",
                SampleReceivedOn = Convert.ToDateTime("2021-03-10"),
                SampleTestedOn = Convert.ToDateTime("2021-03-11"),
                ReportCreatedOn = Convert.ToDateTime("2021-03-12"),
                TestResult = 1005,
                NeedConsultation = true,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport3))
            {
                _context.LabReports.Add(LabReport3);
                isValid = true;
            }

            var LabReport4 = new Model.LabReport
            {
                LabTestId = 2,
                PatientId = 4,
                RefferredBy = "Dr. Physician 4",
                SampleReceivedOn = Convert.ToDateTime("2021-04-20"),
                SampleTestedOn = Convert.ToDateTime("2021-04-21"),
                ReportCreatedOn = Convert.ToDateTime("2021-04-22"),
                TestResult = 195,
                NeedConsultation = true,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport4))
            {
                _context.LabReports.Add(LabReport4);
                isValid = true;
            }

            var LabReport5 = new Model.LabReport
            {
                LabTestId = 3,
                PatientId = 1,
                RefferredBy = "Dr. Physician 5",
                SampleReceivedOn = Convert.ToDateTime("2021-05-01"),
                SampleTestedOn = Convert.ToDateTime("2021-05-02"),
                ReportCreatedOn = Convert.ToDateTime("2021-05-03"),
                TestResult = 325,
                NeedConsultation = false,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport5))
            {
                _context.LabReports.Add(LabReport5);
                isValid = true;
            }

            var LabReport6 = new Model.LabReport
            {
                LabTestId = 4,
                PatientId = 3,
                RefferredBy = "Dr. Physician 6",
                SampleReceivedOn = Convert.ToDateTime("2021-06-11"),
                SampleTestedOn = Convert.ToDateTime("2021-06-12"),
                ReportCreatedOn = Convert.ToDateTime("2021-06-13"),
                TestResult = 0,
                NeedConsultation = true,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport6))
            {
                _context.LabReports.Add(LabReport6);
                isValid = true;
            }

            var LabReport7 = new Model.LabReport
            {
                LabTestId = 3,
                PatientId = 2,
                RefferredBy = "Dr. Physician 7",
                SampleReceivedOn = Convert.ToDateTime("2021-01-21"),
                SampleTestedOn = Convert.ToDateTime("2021-07-22"),
                ReportCreatedOn = Convert.ToDateTime("2021-07-23"),
                TestResult = 3050,
                NeedConsultation = true,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport7))
            {
                _context.LabReports.Add(LabReport7);
                isValid = true;
            }

            var LabReport8 = new Model.LabReport
            {
                LabTestId = 4,
                PatientId = 4,
                RefferredBy = "Dr. Physician 8",
                SampleReceivedOn = Convert.ToDateTime("2021-08-11"),
                SampleTestedOn = Convert.ToDateTime("2021-08-12"),
                ReportCreatedOn = Convert.ToDateTime("2021-08-13"),
                TestResult = 0,
                NeedConsultation = true,
                isDeleted = false
            };

            if (await IsValidPatientnLabTest(LabReport8))
            {
                _context.LabReports.Add(LabReport8);
                isValid = true;
            }

            if (isValid)
                await _context.SaveChangesAsync();
        }

        private static async Task<bool> IsValidPatientnLabTest(LabReport labReport)
        {
            var patient = await _context.Patients.FindAsync(labReport.PatientId);
            var labTest = await _context.LabTests.FindAsync(labReport.LabTestId);

            if (patient == null || labTest == null)
                return false;

            return true;
        }
    }
}
