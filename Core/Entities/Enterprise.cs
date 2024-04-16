namespace Core.Entities;

public class Enterprise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<PromotionEnterprise> PromotionsEnterprises { get; set; } = new List<PromotionEnterprise>();

}
