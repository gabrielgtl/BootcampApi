using Core.Entities;

namespace Core.Models;

public class PaymentDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string? Description { get; set; }
    public string Service { get; set; } = string.Empty;
    public string AccountHolder { get; set; } = string.Empty;

}
