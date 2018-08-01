using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Config
{
    class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(ut => new { ut.TeamId, ut.UserId });


            builder.HasOne(ut => ut.User)
              .WithMany(u => u.UserTeams)
              .HasForeignKey(ut => ut.UserId);

            builder.HasOne(ut => ut.Team)
             .WithMany(t => t.UserTeams)
             .HasForeignKey(t => t.TeamId);
        }
    }
}
   

