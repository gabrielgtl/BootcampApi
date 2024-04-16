using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> entity)
    {
        entity.HasKey(e => e.Id);

        entity
            .Property(e => e.Name)
            .IsRequired();

        entity
            .Property(e => e.Address)
            .IsRequired();

        entity
            .Property(e => e.Phone)
            .IsRequired();

        entity
            .Property(e => e.Email)
            .IsRequired();

        entity
            .HasMany(e => e.PromotionsEnterprises)
            .WithOne(pe => pe.Enterprise)
            .HasForeignKey(pe => pe.EnterpriseId);
    }
}
