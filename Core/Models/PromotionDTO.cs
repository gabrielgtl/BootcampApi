using Core.Entities;

namespace Core.Models;

public class PromotionDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DurationTime { get; set; }
    public decimal DiscountPercentage { get; set; }
    public BusinessDTO Business { get; set; } = null!;

}
