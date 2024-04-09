using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> entity)
    {
        entity.ToTable("Currencies");
        entity
            .HasKey(e => e.Id)
            .HasName("Currency_pkey");

        entity
            .Property(e => e.Name)
            .HasMaxLength(50);

        entity
            .Property(e => e.BuyValue)
            .HasColumnType("numeric(20,5)");

        entity
            .Property(e => e.SellValue)
            .HasColumnType("numeric(20,5)");

        entity
            .HasMany(currency => currency.Accounts)
            .WithOne(account => account.Currency)
            .HasForeignKey(account => account.CurrencyId);
    }
}