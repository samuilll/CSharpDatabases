using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class TrainClassConfig : IEntityTypeConfiguration<TrainSeats>
    {
        public void Configure(EntityTypeBuilder<TrainSeats> builder)
        {
            builder.HasKey(ts => new { ts.TrainId, ts.SeatingClassId });

            builder.HasOne(ts => ts.Train)
                .WithMany(t => t.TrainSeatClasses)
                .HasForeignKey(ts => ts.TrainId);

            builder.HasOne(ts => ts.SeatingClass)
               .WithMany(st => st.TrainSeatClasses)
               .HasForeignKey(ts => ts.SeatingClassId);

        }
    }
}
