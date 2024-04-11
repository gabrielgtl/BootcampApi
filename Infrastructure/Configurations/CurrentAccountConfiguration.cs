using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CurrentAccountConfiguration : IEntityTypeConfiguration<CurrentAccount>
{
    public void Configure(EntityTypeBuilder<CurrentAccount> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("CurrentAccount_pkey");

        entity
            .Property(e => e.Interest)
            .HasColumnType("numeric(10,5)");

        entity
            .Property(e => e.OperationalLimit)
            .HasColumnType("numeric(20,5)");

        entity
            .Property(e => e.MonthAverage)
            .HasColumnType("numeric(20,5)");

        entity
            .HasOne(d => d.Account)
            .WithOne(p => p.CurrentAccount)
            .HasForeignKey<CurrentAccount>(d => d.AccountId);
    }
}