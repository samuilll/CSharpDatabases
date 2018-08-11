using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasOne(t => t.OriginStation)
                .WithMany(s => s.Departures)
                .HasForeignKey(t => t.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationStation)
               .WithMany(s => s.Arrivals)
               .HasForeignKey(t => t.DestinationStationid)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Train)
                .WithMany(t => t.Trips)
                .HasForeignKey(t => t.TrainId);

            builder.HasMany(tr => tr.Tickets)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId);

            builder.Property(t => t.Status)
                .HasDefaultValue(TripStatus.OnTime);
        }
    }
}
