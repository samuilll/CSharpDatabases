using Cars.Data.Data.Models;
using Cars.Data.Data.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Data
{
  public  class CarsDbContext:DbContext
    {
        public CarsDbContext()
        {

        }

        public CarsDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Dealership> Dealerships { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<LicensePlate> LicensePlates { get; set; }
        public DbSet<Make> Makes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfiguration(new CarDealerShipConfiguration());

            builder.ApplyConfiguration(new CarConfiguration());

            base.OnModelCreating(builder);
        }


    }
}
