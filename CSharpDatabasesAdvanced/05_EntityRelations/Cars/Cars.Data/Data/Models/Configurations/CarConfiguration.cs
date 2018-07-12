using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Data.Models.Configurations
{
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasOne(c => c.Plate)
                .WithOne(lp => lp.Car)
                .HasForeignKey<LicensePlate>(lp => lp.CarId);

            builder.HasMany(c => c.CarDealerships)
                .WithOne(cd => cd.Car)
                .HasForeignKey(cd => cd.CarId);

            builder.HasOne(c => c.Engine)
                .WithMany(en => en.Cars)
                .HasForeignKey(c => c.EngineId);

            builder.HasOne(c => c.Make)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.MakeId);
        }
    }
}
