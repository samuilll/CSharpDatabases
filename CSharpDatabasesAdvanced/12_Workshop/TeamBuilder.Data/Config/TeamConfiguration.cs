using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Config
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasOne(t => t.Creator)
                .WithMany(c => c.CreatedTeams)
                .HasForeignKey(t=>t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
