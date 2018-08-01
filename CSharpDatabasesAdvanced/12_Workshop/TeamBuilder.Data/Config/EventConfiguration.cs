using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Config
{
    class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                .IsUnicode(true);

            builder.HasOne(e => e.Creator)
                .WithMany(c => c.CreatedEvents)
                .HasForeignKey(e => e.CreatorId);

        }
    }
}
