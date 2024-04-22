using Core.Entities;

namespace Core.Entities;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();


}
