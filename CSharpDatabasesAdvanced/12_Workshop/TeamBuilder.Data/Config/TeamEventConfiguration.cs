using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Config
{
    class TeamEventConfiguration : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasKey(te => new { te.EventId, te.TeamId });

            builder.HasOne(te => te.Team)
                .WithMany(t => t.EventTeams)
                .HasForeignKey(te => te.TeamId);

            builder.HasOne(te => te.Event)
              .WithMany(e => e.EventTeams)
              .HasForeignKey(te => te.EventId);
        }
    }
}
