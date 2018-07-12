using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Config
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasOne(t => t.PrimaryKitColor)
                .WithMany(pkc => pkc.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.SecondaryKitColor)
                .WithMany(skc => skc.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Initials)
                .HasColumnType("CHAR(3)");

            
            builder.HasOne(t => t.Town)
                .WithMany(tn => tn.Teams)
                .HasForeignKey(t => t.TownId);

        }
    }
}
