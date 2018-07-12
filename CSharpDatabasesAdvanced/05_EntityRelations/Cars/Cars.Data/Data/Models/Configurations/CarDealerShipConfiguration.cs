using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Data.Models.Configurations
{
    public class CarDealerShipConfiguration : IEntityTypeConfiguration<CarDealership>
    {
        public void Configure(EntityTypeBuilder<CarDealership> builder)
        {
            builder.HasOne(cd => cd.Car)
               .WithMany(c => c.CarDealerships)
               .HasForeignKey(cd => cd.CarId);

            builder.HasOne(cd => cd.Dealership)
              .WithMany(d => d.DealershipCars)
              .HasForeignKey(cd => cd.DealershipId);

            builder.HasKey(cd => new { cd.CarId, cd.DealershipId });
        }
    }
}
