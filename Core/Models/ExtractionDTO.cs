namespace Core.Models;

public class ExtractionDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }

    public string HolderName { get; set; } = string.Empty;
}
