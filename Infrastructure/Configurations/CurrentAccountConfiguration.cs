using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            .HasPrecision(10, 5);

        entity
            .Property(e => e.OperationalLimit)
            .HasPrecision(20, 5);

        entity
            .Property(e => e.MonthAverage)
            .HasPrecision(20, 5);

        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.CurrentAccounts)
            .HasForeignKey(d => d.AccountId);
    }
}
