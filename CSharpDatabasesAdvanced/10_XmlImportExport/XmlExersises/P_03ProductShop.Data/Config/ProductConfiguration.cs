using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P_03ProductShop.Models;

namespace P_03ProductShop.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired(true);

            builder.Property(p => p.Price)
              .IsRequired(true);

            builder.Property(p => p.SellerId)
              .IsRequired(true);

            builder.HasOne(p=>p.Buyer)
                .WithMany(u=>u.BuiedProducts)
                .HasForeignKey(p=>p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Seller)
                .WithMany(u => u.ProductsToSell)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.CategoryProducts)
              .WithOne(cp => cp.Product)
              .HasForeignKey(cp => cp.ProductId);
        }
    }
}
