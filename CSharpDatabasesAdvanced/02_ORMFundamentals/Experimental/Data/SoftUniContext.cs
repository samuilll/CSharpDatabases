using Experimental.Data.Entities;
using MiniORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Experimental.Data
{
   public class SoftUniContext:DbContext
    {
        public SoftUniContext(string connectionString)
          : base(connectionString)
        {
        }

        public DbSet<Employee> Employees { get;}
        public DbSet<Department> Departments { get; }
        public DbSet<Project> Projects { get; }
        public DbSet<EmployeeProject> EmployeesProjects { get; }

    }
}
