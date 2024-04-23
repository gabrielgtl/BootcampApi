namespace Core.Entities;

public class Extraction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}
