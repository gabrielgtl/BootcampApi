namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime OperationDate { get; set; }
    public string? Description { get; set; }


    public int AccountId { get; set; }
    public int ServiceId { get; set; }

    public Service Service { get; set; } = null!;
    public Account Account { get; set; } = null!;
}