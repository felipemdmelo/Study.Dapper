using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
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
            //_connection.Open();

            var departments = await _connection.GetAllAsync<Department>();

            return departments.ToList();
        }

        public async Task<Department> Get(long id)
        {
            //_connection.Open();

            var department = await _connection.GetAsync<Department>(id);

            return department;
        }

        public async Task<long> Insert(Department department)
        {
            var identity = await _connection.InsertAsync(department, transaction: _transaction);

            return identity;
        }

        public async Task<bool> Update(Department department)
        {
            //_connection.Open();

            var isSuccess = await _connection.UpdateAsync(department);

            return isSuccess;
        }

        public async Task<bool> Delete(Department department)
        {
            //_connection.Open();

            var isSuccess = await _connection.DeleteAsync(department);

            return isSuccess;
        }
    }
}
