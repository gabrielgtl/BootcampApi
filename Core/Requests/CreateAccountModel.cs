using Core.Constants;
using Core.Entities;
using Core.Models;

namespace Core.Requests;

public class CreateAccountModel
{
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public int CustomerId { get; set; }

    public decimal? OperationalLimit { get; set; }
    public decimal? MonthAverage { get; set; }
    public decimal? Interest { get; set; }

    public SavingType SavingType { get; set; }


}
