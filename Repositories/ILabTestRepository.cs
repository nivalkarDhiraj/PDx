using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Repositories
{
    public interface ILabTestRepository
    {
        Task<IEnumerable<LabTest>> Get();

        Task<LabTest> Get(int id);

        Task<LabTest> Create(LabTest labTest);

        Task<bool> Update(LabTest labTest);

        Task Delete(int id);

        Task Restore(int id);
    }
}
