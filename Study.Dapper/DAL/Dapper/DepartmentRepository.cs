using Dapper.Contrib.Extensions;
using Study.Dapper.DL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Study.Dapper.DAL.Dapper
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public DepartmentRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<List<Department>> GetAll()
        {
            var departments = await _connection.GetAllAsync<Department>(transaction: _transaction);

            return departments.ToList();
        }

        public async Task<Department> Get(long id)
        {
            var department = await _connection.GetAsync<Department>(id, transaction: _transaction);

            return department;
        }

        public async Task<long> Insert(Department department)
        {
            return await _connection.InsertAsync(department, transaction: _transaction);
        }

        public async Task<bool> Update(Department department)
        {
            return await _connection.UpdateAsync(department, transaction: _transaction);
        }

        public async Task<bool> Delete(Department department)
        {
            return await _connection.DeleteAsync(department, transaction: _transaction);
        }
    }
}
