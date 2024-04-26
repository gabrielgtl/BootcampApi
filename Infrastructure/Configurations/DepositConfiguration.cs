using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> entity)
        {
            entity
                .HasKey(e => e.Id)
                .HasName("Deposit_pkey");

            entity
                .Property(e => e.Amount)
                .HasPrecision(20, 5)
                .IsRequired();

            entity
                .Property(e => e.Description)
                .HasMaxLength(500);
            entity
                .HasOne(d => d.Account)
                .WithMany(p => p.Deposits)
                .HasForeignKey(d => d.AccountId);
        }
    }
}
