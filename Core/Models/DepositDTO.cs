namespace Core.Models;

public class DepositDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string Description { get; set; } = string.Empty;

    public string HolderName { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;

}
