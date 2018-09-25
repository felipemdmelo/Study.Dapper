using Study.Dapper.DL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Study.Dapper.DAL.Dapper
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();
        Task<Department> Get(long id);
        Task<long> Insert(Department department);
        Task<bool> Update(Department department);
        Task<bool> Delete(Department department);
    }
}
