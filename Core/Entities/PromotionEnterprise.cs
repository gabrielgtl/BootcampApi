namespace Core.Entities;

public class PromotionEnterprise
{
    public int PromotionId { get; set; }
    public int EnterpriseId { get; set; }
    public Promotion Promotion { get; set; } = null!;
    public Enterprise Enterprise { get; set; } = null!;
}
