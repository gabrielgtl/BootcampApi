using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

public class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Movimientos_pkey");

        entity
            .Property(e => e.Destination)
            .HasMaxLength(150)
            .IsRequired();
        entity
            .Property(e => e.TransferredDateTime)
            .HasColumnType("timestamp without time zone");
        entity
            .Property(e => e.Amount)
            .HasPrecision(20, 5);
        entity
            .Property(e => e.TransferStatus)
            .HasMaxLength(50);

        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.Movements)
            .HasForeignKey(d => d.AccountId);
    }
}
