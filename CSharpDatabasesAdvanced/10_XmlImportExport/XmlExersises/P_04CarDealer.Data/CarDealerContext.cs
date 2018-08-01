using Microsoft.EntityFrameworkCore;
using P_04CarDealer.Data.Config;
using P_04CarDealer.Models;
using System;

namespace P_04CarDealer.Data
{
    public class CarDealerContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<PartCar> PartCars { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public CarDealerContext()
        {

        }

        public CarDealerContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseLazyLoadingProxies().UseSqlServer(ConnectionConfiguration.ConnectionString);
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new PartCarConfiguration());
            builder.ApplyConfiguration(new PartConfiguration());
            builder.ApplyConfiguration(new SaleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
