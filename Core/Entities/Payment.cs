namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }


    public decimal Amount { get; set; } 


    // Relación con la cuenta de origen
    public int SourceAccountId { get; set; }
    public Account Account { get; set; } = null!;
}