using Study.Dapper.DL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Study.Dapper.DAL.Dapper
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAll();
        Task<Course> Get(long id);
        Task<long> Insert(Course course);
        Task<bool> Update(Course course);
        Task<bool> Delete(Course course);
    }
}
