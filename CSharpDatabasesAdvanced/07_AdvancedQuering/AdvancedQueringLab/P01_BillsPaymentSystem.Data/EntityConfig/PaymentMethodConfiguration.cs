using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasIndex(pm=>new { pm.UserId,pm.BankAccountId,pm.CreditCardId}).IsUnique(true);

            builder.HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm => pm.UserId)
                .IsRequired(true);

            builder.HasOne(pm => pm.BankAccount)
                .WithOne(bk => bk.PaymentMethod)
                .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId)
                .IsRequired(false);

            builder.HasOne(pm => pm.CreditCard)
                .WithOne(cc => cc.PaymentMethod)
                .HasForeignKey<PaymentMethod>(pm => pm.CreditCardId)
                .IsRequired(false);
        }
    }
}
