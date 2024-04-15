using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Business_pkey");
        entity
            .Property(e => e.Name)
            .HasMaxLength(100).IsRequired();

        entity
            .Property(e => e.Address)
            .HasMaxLength(100);


        entity
            .Property(e => e.Phone)
            .HasMaxLength(100);

        entity
            .Property(e => e.Email)
            .HasMaxLength(100);
        entity
            .HasMany(b => b.Promotions)
            .WithOne(c => c.Business)
            .HasForeignKey(business => business.BusinessId);
    }
}
