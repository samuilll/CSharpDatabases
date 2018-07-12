
namespace P03_SalesDatabase.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Product> Products { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionbString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(e =>
            {
                e.HasMany(p => p.Sales)
                .WithOne(s => s.Product)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("FK_Sale_Product");

                e.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

                e.Property(p => p.Description)
               .IsRequired(false)
               .HasDefaultValue("No description")
               .HasMaxLength(250)
               .IsUnicode(true);

            });

            builder.Entity<Customer>(e =>
            {
                e.HasMany(p => p.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_Sale_Customer");

                e.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

                e.Property(c => c.Email)
               .IsRequired(true)
               .HasMaxLength(80)
               .IsUnicode(true);

            });

            builder.Entity<Store>(e =>
            {
                e.HasMany(s => s.Sales)
                .WithOne(sale => sale.Store)
                .HasForeignKey(sale => sale.StoreId)
                .HasConstraintName("FK_Sale_Store");

                e.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(80)
                .IsUnicode(true);

            });

            builder.Entity<Sale>(e =>
            {


                e.Property(s => s.Date)
                .IsRequired(true)
                .HasDefaultValue(DateTime.Now);


            });

        }


    }
}
