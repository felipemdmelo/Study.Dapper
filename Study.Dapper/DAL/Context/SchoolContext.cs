using Microsoft.EntityFrameworkCore;
using Study.Dapper.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study.Dapper.DAL.Context
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }

        public SchoolContext()
        {

        }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            //Database.Migrate();
        }
    }
}
