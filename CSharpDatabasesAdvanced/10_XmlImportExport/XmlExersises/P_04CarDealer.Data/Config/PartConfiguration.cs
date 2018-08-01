using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P_04CarDealer.Models;

namespace P_04CarDealer.Data.Config
{
    class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p=>p.SupplierId);
        }
    }
}
