

using Core.Constants;

namespace Core.Entities;

public class CreditCard
{
    public int Id {get; set;}
    public string Designation {get; set;} = string.Empty;
    public DateTime IssueDate {get; set;}
    public DateTime ExpirationDate {get; set;}
    public string CardNumber { get; set; } = string.Empty;
    public int Cvv {get; set;}
    public CreditCardStatus CreditCardStatus { get; set; } = CreditCardStatus.Enabled;
    public decimal CreditLimit {get; set;}
    public decimal AvailableCredit {get; set;}
    public decimal CurrentDebt {get; set;}
    public decimal InterestRate {get; set;}


    public int ClientId { get; set; }
    public int CurrencyId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
    public virtual Currency Currency { get; set; } = null!;
}
