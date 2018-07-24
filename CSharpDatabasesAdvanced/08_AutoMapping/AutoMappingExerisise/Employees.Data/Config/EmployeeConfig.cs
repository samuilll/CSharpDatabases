using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Data.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
     
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName).IsRequired(true);
            builder.Property(e => e.Lastname).IsRequired(true);
            builder.Property(e => e.Salary).IsRequired(true);

            //builder.HasMany(e => e.Employees)
            //    .WithOne(e => e.Manager)
            //    .HasForeignKey(e => e.ManagerId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.ManagerId);


        }
    }
}
