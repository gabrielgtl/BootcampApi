namespace Core.Requests;

public class CreatePromotionModel
{
    public string Name { get; set; } = string.Empty;
    public int DurationTime { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int BusinessId { get; set; }
}
