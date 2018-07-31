using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P_04CarDealer.Models;

namespace P_04CarDealer.Data.Config
{
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasMany(c => c.Sales)
                .WithOne(s => s.Car)
                .HasForeignKey(s => s.CarId);
        }
    }
}
