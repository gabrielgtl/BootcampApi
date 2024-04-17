using Core.Entities;

namespace Core.Requests.CurrentAccount;

public class CreateCurrentAccount
{
    public decimal? OperationalLimit { get; set; }
    public decimal? MonthAverage { get; set; }
    public decimal? Interest { get; set; }
}
