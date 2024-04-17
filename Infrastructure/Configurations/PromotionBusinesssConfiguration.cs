using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PromotionBusinessConfiguration : IEntityTypeConfiguration<PromotionEnterprise>
{
    public void Configure(EntityTypeBuilder<PromotionEnterprise> entity)
    {
        entity.HasKey(e => new { e.PromotionId, e.EnterpriseId });

        entity
            .HasOne(e => e.Promotion)
            .WithMany(e => e.PromotionsEnterprises)
            .HasForeignKey(e => e.PromotionId);

        entity
            .HasOne(e => e.Enterprise)
            .WithMany(e => e.PromotionsEnterprises)
            .HasForeignKey(e => e.EnterpriseId);
    }
}
