using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travelling.Models;

namespace Travelling.Data.Config
{
    public class CardConfig : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.Property(c => c.CardType)
                .HasDefaultValue(CardType.Normal);
        }
    }
}
