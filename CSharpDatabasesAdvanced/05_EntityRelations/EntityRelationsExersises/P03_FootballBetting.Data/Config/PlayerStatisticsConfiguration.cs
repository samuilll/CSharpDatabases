using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Config
{
    class PlayerStatisticsConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);

            builder.HasOne(ps => ps.Game)
               .WithMany(g => g.PlayerStatistics)
               .HasForeignKey(ps => ps.GameId);

            builder.HasKey(ps=>new { ps.GameId,ps.PlayerId});
        }
    }
}
