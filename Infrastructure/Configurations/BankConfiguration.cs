
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Bank_pkey");

        entity
            .Property(e => e.Address)
            .HasMaxLength(400)
            .IsRequired();

        entity
            .Property(e => e.Mail)
            .HasMaxLength(100)
            .IsRequired();

        entity
            .Property(e => e.Name)
            .HasMaxLength(300)
            .IsRequired();

        entity
            .Property(e => e.Phone)
            .HasMaxLength(150)
            .IsRequired();

        entity
            .HasMany(b => b.Customers)
            .WithOne(c => c.Bank)
            .HasForeignKey(bank => bank.BankId);
    }
}