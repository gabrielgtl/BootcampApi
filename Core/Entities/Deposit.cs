namespace Core.Entities;

public class Deposit
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string Description { get; set; } = string.Empty;


    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}
