using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
public void Configure(EntityTypeBuilder<Service> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Service_pkey");

        entity
            .Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        entity
            .HasMany(b => b.Payments)
            .WithOne(c => c.Service)
            .HasForeignKey(b => b.ServiceId);
    }
}
