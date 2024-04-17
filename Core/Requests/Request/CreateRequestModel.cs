using Core.Constants;
using Core.Entities;

namespace Core.Requests.Request;

public class CreateRequestModel
{
    //CreditCard
    public string? Brand { get; set; }

    //Credit
    public int? Term { get; set; }
    public decimal? Amount { get; set; }


    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    public DateTime DateOfSolicitation { get; set; }
    public DateTime? DateOfApproval { get; set; }

    public int ProductId { get; set; }
    public int CurrencyId { get; set; }
}
