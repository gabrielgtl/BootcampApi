namespace Core.Requests.Currency;

public class CreateCurrencyModel
{
    public string Name { get; set; } = string.Empty;
    public decimal BuyValue { get; set; }
    public decimal SellValue { get; set; }
}
