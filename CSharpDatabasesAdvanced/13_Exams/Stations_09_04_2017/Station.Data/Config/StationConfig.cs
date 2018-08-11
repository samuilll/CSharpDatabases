using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasAlternateKey(s => s.Name);

            builder.Property(s => s.Name)
                .HasMaxLength(50);

            builder.Property(s => s.Town)
                .HasMaxLength(50);

        }
    }
}
