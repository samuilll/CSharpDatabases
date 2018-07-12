using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.BankAccountId);

            builder.Property(ba => ba.Balance)
                .IsRequired(true);

            builder.Property(ba => ba.BankName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(ba => ba.SWIFTCode)
              .HasMaxLength(20)
              .IsUnicode(false)
              .IsRequired(true);



          //  builder.HasIn
        }
    }
}
