using Core.Constants;

namespace Core.Requests;

public class FilterAccountModel
{
    public string Number { get; set; } = string.Empty;
    public string? Type { get; set; }
    public int? CurrencyId { get; set; }
}
