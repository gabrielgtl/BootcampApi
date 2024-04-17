using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> entity)
    {
        entity.HasKey(e => e.Id).HasName("Request_pkey");
        entity
            .Property(e => e.Brand)
            .HasMaxLength(100);
        entity
            .Property(e => e.Amount)
            .HasColumnType("numeric(10,5)");
        entity
            .Property(e => e.Term)
            .HasColumnType("numeric(20,5)");

        entity
            .HasOne(request => request.Currency)
            .WithMany(currency => currency.Requests)
            .HasForeignKey(request => request.CurrencyId);

        entity
            .HasOne(request => request.Product)
            .WithMany(product => product.Requests)
            .HasForeignKey(request => request.ProductId);
    }
}
