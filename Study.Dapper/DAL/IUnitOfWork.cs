using Study.Dapper.DAL.Dapper;
using System.Threading.Tasks;

namespace Study.Dapper.DAL
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Dispose();
        ICourseRepository CourseRepository();
        IDepartmentRepository DepartmentRepository();
    }
}
