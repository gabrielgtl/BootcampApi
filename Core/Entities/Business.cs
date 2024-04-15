namespace Core.Entities;

public class Business
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; } 
    public string? Email {  get; set; }
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();


}
