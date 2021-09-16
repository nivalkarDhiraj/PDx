using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Data;
using HCA.API.LabTests.Model;
using Microsoft.EntityFrameworkCore;

namespace HCA.API.LabTests.Repositories
{
    public class LabReportRepository : ILabReportRepository
    {
        private readonly LabReportContext _context;
        public LabReportRepository(LabReportContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create report
        /// </summary>
        /// <param name="labReport"></param>
        /// <returns></returns>
        public async Task<LabReport> Create(LabReport labReport)
        {
            if (!await IsValidPatientnLabTest(labReport)) //check for patient and test information is available
                return null;

            var labTest = await _context.LabTests.FindAsync(labReport.LabTestId); //get test information
            
            //check if need consultation based on report value against permissible limits/test type
            labReport.NeedConsultation = !((labReport.TestResult > labTest.MinLimit && labReport.TestResult < labTest.MaxLimit) ||
                                           (labTest.TestType == TestTypes.PhysicalTest));
            labReport.isDeleted = false; //active

            _context.LabReports.Add(labReport);
            await _context.SaveChangesAsync();

            return labReport;
        }

        /// <summary>
        /// Delete report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var labReportToDelete = await _context.LabReports.FindAsync(id);
            labReportToDelete.isDeleted = true; //soft deleted

            _context.Entry(labReportToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all reports
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LabReport>> Get()
        {
            var labReports = await _context.LabReports.ToListAsync();

            if (!labReports.Any())
            {
                //create sample data for testing
                LabReportsData._context = _context;
                await LabReportsData.CreateSampleReports();

                labReports = await _context.LabReports.ToListAsync(); //list for sample data
            }

            return labReports.Where(x => !x.isDeleted).ToList(); //list active only
        }

        /// <summary>
        /// Get specific report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LabReport> Get(int id)
        {
            var labReport = await _context.LabReports.FindAsync(id);
            return labReport;
        }

        /// <summary>
        /// Get report based from Test within date range
        /// </summary>
        /// <param name="labTestId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> Get(int labTestId, DateTime from, DateTime to)
        {
            //get patient and test details, get active required test records within date range
            var FindReports = await _context.LabReports.Include(l => l.LabTest).Include(p => p.Patient)
                .Where(x => !x.isDeleted && x.LabTestId == labTestId && (x.ReportCreatedOn >= from && x.ReportCreatedOn <= to)).ToListAsync();

            var labReports = GetFormatedReport(FindReports); //format as readable
            return labReports.ToList();
        }

        /// <summary>
        /// Restore deleted report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Restore(int id)
        {
            var labReportToRestore = await _context.LabReports.FindAsync(id);
            labReportToRestore.isDeleted = false; //active

            _context.Entry(labReportToRestore).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update report
        /// </summary>
        /// <param name="labReport"></param>
        /// <returns></returns>
        public async Task<bool> Update(LabReport labReport)
        {
            var existingLabReport = await _context.LabReports.FindAsync(labReport.Id);
            if (existingLabReport == null || existingLabReport.isDeleted) //check for active
                return false;

            if (!await IsValidPatientnLabTest(labReport)) //check for patient and test details
                return false;

            MapLabReport(existingLabReport, labReport); //map current instance with context instance

            var labTest = await _context.LabTests.FindAsync(labReport.LabTestId);

            //check if need consultation based on report value against permissible limits/test type
            existingLabReport.NeedConsultation = !((labReport.TestResult > labTest.MinLimit && labReport.TestResult < labTest.MaxLimit) ||
                                                   (labTest.TestType == TestTypes.PhysicalTest));
            existingLabReport.isDeleted = false; //active

            _context.Entry(existingLabReport).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Map report with report in current context
        /// </summary>
        /// <param name="existingLabReport"></param>
        /// <param name="modifiedLabReport"></param>
        private void MapLabReport(LabReport existingLabReport, LabReport modifiedLabReport)
        {
            existingLabReport.LabTestId = modifiedLabReport.LabTestId;
            existingLabReport.PatientId = modifiedLabReport.PatientId;
            existingLabReport.RefferredBy = modifiedLabReport.RefferredBy;
            existingLabReport.ReportCreatedOn = modifiedLabReport.ReportCreatedOn;
            existingLabReport.SampleReceivedOn = modifiedLabReport.SampleReceivedOn;
            existingLabReport.SampleTestedOn = modifiedLabReport.SampleTestedOn;
            existingLabReport.TestResult = modifiedLabReport.TestResult;
        }

        /// <summary>
        /// Check active patient and test
        /// </summary>
        /// <param name="labReport"></param>
        /// <returns></returns>
        private async Task<bool> IsValidPatientnLabTest(LabReport labReport)
        {
            var patient = await _context.Patients.FindAsync(labReport.PatientId); //patient exists
            var labTest = await _context.LabTests.FindAsync(labReport.LabTestId); //test exists

            if (patient == null || patient.isDeleted || labTest == null || labTest.isDeleted)
                return false; //no active found

            return true;
        }

        /// <summary>
        /// Get formatted report
        /// </summary>
        /// <param name="FindReports"></param>
        /// <returns></returns>
        private static IEnumerable<object> GetFormatedReport(IEnumerable<LabReport> FindReports)
        {
            return FindReports.Select(n => new
            {
                id = n.Id,
                labTestId = n.LabTest.Description,
                TestType = n.LabTest.TestType.ToString(),
                sampleType = n.LabTest.SampleType.ToString(),
                patientId = n.Patient.PatientName,
                Gender = n.Patient.PatientGender.ToString(),
                sampleReceivedOn = n.SampleReceivedOn,
                sampleTestedOn = n.SampleTestedOn,
                reportCreatedOn = n.ReportCreatedOn,
                testResult = n.TestResult,
                refferredBy = n.RefferredBy,
                NeedConsultation = n.NeedConsultation ? "Yes" : "No"
            });
        }
    }
}
