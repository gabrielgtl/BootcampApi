namespace Core.Requests.Extraction;

public class CreateExtractionModel
{
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string Description { get; set; }
    public int BankId { get; set; }

    public int AccountId { get; set; }
}
