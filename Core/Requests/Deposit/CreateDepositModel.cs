namespace Core.Requests.Deposit;

public class CreateDepositModel
{
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public int BankId { get; set; }

    public int AccountId { get; set; }
}
