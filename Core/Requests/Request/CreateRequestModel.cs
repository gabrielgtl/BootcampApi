using Core.Constants;

namespace Core.Requests.Request;

public class CreateRequestModel
{
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    public DateTime DateOfSolicitation { get; set; }

    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int CurrencyId { get; set; }
}
