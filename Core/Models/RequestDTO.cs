using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class RequestDTO
{
    public int Id { get; set; }

    //CreditCard
    public string? Brand { get; set; } = string.Empty;

    //Credit
    public int? Term { get; set; }
    public decimal? Amount { get; set; }

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    public DateTime DateOfSolicitation { get; set; }
    public DateTime? DateOfApproval { get; set; }
    public virtual ProductDTO ProductDTO { get; set; } = null!;
    public virtual CurrencyDTO CurrencyDTO { get; set; } = null!;
}
