using Microsoft.EntityFrameworkCore;
using Travelling.Data.Config;
using System;
using Travelling.Models;

namespace Travelling.Data
{
    public class TravellingContext : DbContext
    {
        public DbSet<CustomerCard> CustomerCards { get; set; }
        public DbSet<SeatingClass> SeatingClasses { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainSeats> TrainSeatClasses { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public TravellingContext()
        {

        }

        public TravellingContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseLazyLoadingProxies().UseSqlServer(ConnectionConfig.ConnectionString);
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CardConfig());
            builder.ApplyConfiguration(new SeatingClassConfig());
            builder.ApplyConfiguration(new StationConfig());
            builder.ApplyConfiguration(new TrainClassConfig());
            builder.ApplyConfiguration(new TrainConfig());
            builder.ApplyConfiguration(new TripConfig());

            base.OnModelCreating(builder);
        }
    }
}
