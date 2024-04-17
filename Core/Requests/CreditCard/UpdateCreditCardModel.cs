using Core.Constants;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.CreditCard;

public class UpdateCreditCardModel
{
    public int Id { get; set; }
    public string Designation { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CardNumber { get; set; } = string.Empty;
    public int Cvv { get; set; }
    public string CreditCardStatus { get; set; } = string.Empty;
    public decimal CreditLimit { get; set; }
    public decimal AvailableCredit { get; set; }
    public decimal CurrentDebt { get; set; }
    public decimal InterestRate { get; set; }
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
}
