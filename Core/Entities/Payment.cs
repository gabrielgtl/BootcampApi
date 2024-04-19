namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; } 
    public string? Description { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}