using Dapper;
using Dapper.Contrib.Extensions;
using Study.Dapper.DL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Study.Dapper.DAL.Dapper
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public CourseRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<List<Course>> GetAll()
        {
            string sqlQuery = "select * from Courses c inner join Departments d on c.DepartmentId = d.Id;";

            var courses = await _connection.QueryAsync<Course, Department, Course>(sqlQuery,
                    (course, department) =>
                    {
                        course.Department = department;
                        return course;
                    },
                    splitOn: "DepartmentId", 
                    transaction: _transaction);
            /*
             * Pode não fazer sentido passar uma transação em modo consulta, mas o seguinte precisa ser considerado:
             * 1 - Caso _transaction seja null, o Dapper o ignora e realiza a consulta normalmente
             * 2 - Caso tenha sido iniciada uma transação e seja preciso realizar uma consulta durante o percurso, o Dapper lança uma exceção exigindo
             *     que a consulta também contenha a transação que está em aberta
             */

            return courses.ToList();
        }

        public async Task<long> Insert(Course course)
        {
            return await _connection.InsertAsync(course, transaction: _transaction);
        }

        public async Task<Course> Get(long id)
        {
            string sqlQuery = "select * from Courses c inner join Departments d on c.DepartmentId = d.Id WHERE c.Id = @CourseId;";

            var courses = await _connection.QueryAsync<Course, Department, Course>(sqlQuery,
                    (course, department) =>
                    {
                        course.Department = department;
                        return course;
                    },
                    splitOn: "DepartmentId",
                    param: new { CourseId = id }, 
                    transaction: _transaction);

            return courses.SingleOrDefault();
        }

        public async Task<bool> Update(Course course)
        {
            return await _connection.UpdateAsync(course, transaction: _transaction);
        }

        public async Task<bool> Delete(Course course)
        {
            return await _connection.DeleteAsync(course, transaction: _transaction);
        }
    }
}
