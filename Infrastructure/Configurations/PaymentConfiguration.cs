using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Payment_pkey");

        entity
            .Property(e => e.Description)
            .HasMaxLength(500);

        entity
            .Property(e => e.Amount)
            .HasColumnType("numeric(10,5)")
            .IsRequired();

        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.AccountId);
    }
}
