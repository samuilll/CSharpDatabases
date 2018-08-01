using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.Data.Config;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<TeamEvent> TeamEvents { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<Event> Events { get; set; }



        public TeamBuilderContext()
        {

        }

        public TeamBuilderContext(DbContextOptions options)
            :base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseLazyLoadingProxies().UseSqlServer(DatabaseConfig.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new TeamEventConfiguration());
            builder.ApplyConfiguration(new UserTeamConfiguration());

            base.OnModelCreating(builder);
        }

    }
}
