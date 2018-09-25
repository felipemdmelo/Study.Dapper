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

            return courses.ToList();
        }

        public async Task<long> Insert(Course course)
        {
            var identity = await _connection.InsertAsync(course, transaction: _transaction);

            return identity;
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
                    param: new { CourseId = id });

            return courses.SingleOrDefault();
        }

        public async Task<bool> Update(Course course)
        {
            //connection.Open();

            var isSuccess = await _connection.UpdateAsync(course);

            return isSuccess;
        }

        public async Task<bool> Delete(Course course)
        {
            //connection.Open();

            var isSuccess = await _connection.DeleteAsync(course);

            return isSuccess;
        }
    }
}
