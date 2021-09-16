using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Repositories
{
    public interface ILabReportRepository
    {
        Task<IEnumerable<LabReport>> Get();

        Task<LabReport> Get(int id);

        Task<IEnumerable<object>> Get(int labTestId, DateTime from, DateTime to);

        Task<LabReport> Create(LabReport labReport);

        Task<bool> Update(LabReport labReport);

        Task Delete(int id);

        Task Restore(int id);
    }
}
