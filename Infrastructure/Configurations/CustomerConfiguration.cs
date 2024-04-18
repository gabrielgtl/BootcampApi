using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity
            .HasKey(e => e.Id)
            .HasName("Customer_pkey");

        entity
            .Property(e => e.Lastname)
            .HasMaxLength(300);
        entity
            .Property(e => e.Phone)
            .HasMaxLength(150);
        entity
            .Property(e => e.Address)
            .HasMaxLength(400);
        entity
            .Property(e => e.DocumentNumber)
            .HasMaxLength(150)
            .IsRequired();
        entity
            .Property(e => e.CustomerStatus)
            .HasMaxLength(100);
        entity
            .Property(e => e.Mail)
            .HasMaxLength(100);
        entity
            .Property(e => e.Name)
            .HasMaxLength(300)
            .IsRequired();

        entity
            .HasOne(d => d.Bank)
            .WithMany(p => p.Customers)
            .HasForeignKey(d => d.BankId);

        entity
            .HasMany(d => d.Accounts)
            .WithOne(p => p.Customer)
            .HasForeignKey(d => d.CustomerId);

        entity
            .HasMany(d => d.Requests)
            .WithOne(p => p.Customer)
            .HasForeignKey(d => d.CustomerId);
    }
}
