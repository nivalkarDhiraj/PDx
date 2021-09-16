using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Model;

namespace HCA.API.LabTests.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> Get();

        Task<Patient> Get(int id);

        Task<Patient> Create(Patient patient);

        Task<bool> Update(Patient patient);

        Task Delete(int id);

        Task Restore(int id);
    }
}
