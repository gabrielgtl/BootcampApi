namespace Core.Entities;

public class Promotion
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DurationTime { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}
