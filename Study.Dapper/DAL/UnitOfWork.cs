using Microsoft.Extensions.Configuration;
using Study.Dapper.DAL.Dapper;
using System;
using System.Data.SqlClient;

namespace Study.Dapper.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        private ICourseRepository _courseRepository;
        private IDepartmentRepository _departmentRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection"));
            _connection.Open();
        }

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }

        ICourseRepository IUnitOfWork.CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                    _courseRepository = new CourseRepository(_connection, _transaction);

                return _courseRepository;
            }
        }

        IDepartmentRepository IUnitOfWork.DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                    _departmentRepository = new DepartmentRepository(_connection, _transaction);

                return _departmentRepository;
            }
        }
    }
}
