using Microsoft.EntityFrameworkCore;
using P_03ProductShop.Data.Config;
using P_03ProductShop.Models;
using System;

namespace P_03ProductShop.Data
{
    public class ProductShopContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }

        public ProductShopContext()
        {

        }

        public ProductShopContext(DbContextOptions options)
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
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryProductConfiguration());


            base.OnModelCreating(builder);
        }
    }
}
