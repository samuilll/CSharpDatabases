using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services
{
    using Contracts;
    using Employees.Data;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseInitializeService : IDatabaseInitializeService
    {
        public void InitializeDatabase()
        {
            using (var db = new EmployeeDbContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
