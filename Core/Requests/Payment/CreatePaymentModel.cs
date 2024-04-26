namespace Core.Requests.Payment;

public class CreatePaymentModel
{
    public string DocumentNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string? Description { get; set; }
    public int ServiceId { get; set; }
    public int AccountId { get; set; }
}
