using Employees.Data.Config;
using Employees.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Employees.Data
{
    public class EmployeeDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext()
        {
        }

        public EmployeeDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(DatabaseConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfig());
        }
    }
}
