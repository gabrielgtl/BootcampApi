using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class RequestDTO
{
    public int Id { get; set; }

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    public DateTime DateOfSolicitation { get; set; }
    public DateTime? DateOfApproval { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
}
