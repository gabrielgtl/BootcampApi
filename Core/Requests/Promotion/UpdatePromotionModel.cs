namespace Core.Requests.Promotion;

public class UpdatePromotionModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Discount { get; set; }
    public List<int> Enterprises { get; set; } = new List<int>();

}
