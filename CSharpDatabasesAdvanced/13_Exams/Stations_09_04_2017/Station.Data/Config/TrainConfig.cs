using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class TrainConfig : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasAlternateKey(t => t.TrainNumber);

            builder.Property(t => t.TrainNumber)
                .HasMaxLength(10);
    
        }
    }
}
