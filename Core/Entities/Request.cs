using Core.Constants;

namespace Core.Entities;

public class Request
{
    public int Id { get; set; }

    public string RequestDescription { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    public DateTime DateOfSolicitation { get; set; }
    public DateTime? DateOfApproval { get; set; }

    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public virtual Currency Currency { get; set; } = null!;
}
