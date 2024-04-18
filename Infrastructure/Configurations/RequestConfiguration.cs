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
            .Property(e => e.RequestDescription)
            .HasMaxLength(100)
            .IsRequired();

        entity
            .HasOne(request => request.Currency)
            .WithMany(currency => currency.Requests)
            .HasForeignKey(request => request.CurrencyId);

        entity
            .HasOne(request => request.Customer)
            .WithMany(customer => customer.Requests)
            .HasForeignKey(request => request.CustomerId);

        entity
            .HasOne(request => request.Product)
            .WithMany(product => product.Requests)
            .HasForeignKey(request => request.ProductId);
    }
}
