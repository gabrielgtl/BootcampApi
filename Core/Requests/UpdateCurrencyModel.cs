namespace Core.Requests;

public class UpdateCurrencyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal BuyValue { get; set; }
    public decimal SellValue { get; set; }
}
