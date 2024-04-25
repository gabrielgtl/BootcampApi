using Core.Constants;

namespace Core.Requests.Movements;

public class CreateMovementModel
{
    public DateTime TransferredDateTime { get; set; }
    public TransferStatus TransferStatus { get; set; } = TransferStatus.Pending;
    public MovementType MovementType { get; set; } = MovementType.Transference;
    public decimal Amount { get; set; }
    public int DestinyBankId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public int CurrencyId { get; set; } 

    public int OriginAccountId { get; set; }
    public int DestinationAccountId { get; set; }
}
