namespace Core.Requests;

public class CreatePromotionModel
{
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Discount { get; set; }
    public List<int> Enterprises { get; set; } = new List<int>();
}
