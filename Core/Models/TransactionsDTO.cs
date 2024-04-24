namespace Core.Models;

public class TransactionsDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime? OperationDate { get; set; }
    public string? Description { get; set; }
    public string Type {  get; set; } = string.Empty;

    public int AccountId { get; set; }
}
