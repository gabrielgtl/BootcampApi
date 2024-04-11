using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SavingAccountConfiguration : IEntityTypeConfiguration<SavingAccount>
{
    public void Configure(EntityTypeBuilder<SavingAccount> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("SavingAccount_pkey");

        entity
            .Property(e => e.HolderName)
            .HasMaxLength(100)
            .IsRequired();

        entity
            .HasOne(d => d.Account)
            .WithOne(p => p.SavingAccount)
            .HasForeignKey<SavingAccount>(d => d.AccountId);
    }
}