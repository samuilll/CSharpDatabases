using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Username)
                .IsUnique(true);

            builder.HasMany(u => u.ReceivedInvitations)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.InvitedUserId);

            builder.HasMany(u => u.CreatedEvents)
               .WithOne(i => i.Creator)
               .HasForeignKey(i => i.CreatorId);
        }
    }
}
