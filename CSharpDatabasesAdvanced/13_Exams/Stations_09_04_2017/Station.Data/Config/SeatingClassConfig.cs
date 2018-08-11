using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class SeatingClassConfig : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasAlternateKey(sc => sc.Name);

            builder.Property(sc => sc.Name)
                .HasMaxLength(30);

            builder.Property(sc => sc.Abbreviation)
                .HasColumnType("Char(2)");
        }
    }
}
