using Core.Constants;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductType ProductType { get; set; } = ProductType.CreditCard;

    public ICollection<Request> Requests { get; set; } = new List<Request>();

}
