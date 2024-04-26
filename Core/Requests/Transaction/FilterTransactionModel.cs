namespace Core.Requests.Transaction;

public class FilterTransactionModel
{
    public int AccountId { get; set; }
    public string? Type { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public DateTime? StartDate { get; set;}
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
}
