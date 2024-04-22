using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class MovementDTO
{
    public int Id { get; set; }
    public DateTime? TransferredDateTime { get; set; }
    public TransferStatus TransferStatus { get; set; } = TransferStatus.Pending;
    public MovementType MovementType { get; set; } = MovementType.Transference;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public int AccountDestiny { get; set; }
    public int AccountSource { get; set; } 

    public decimal BalanceOriginAcount { get; set; }
}
