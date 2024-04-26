using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class ExtractionConfiguration : IEntityTypeConfiguration<Extraction>
{
    public void Configure(EntityTypeBuilder<Extraction> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Extraction_pkey");

        entity
            .Property(e => e.Amount)
            .HasPrecision(20, 5)
            .IsRequired();

        entity
            .Property(e => e.Description)
            .HasMaxLength(500);

        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.Extractions)
            .HasForeignKey(d => d.AccountId);
    }
}

