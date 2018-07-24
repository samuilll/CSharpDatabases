using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Data.Config
{
   public class DatabaseConfig
    {
        public const string ConnectionString =
           @"Server=(localdb)\MSSQLLocalDB;Database=EmployeesDatabase;Integrated Security = True";
    }
}
