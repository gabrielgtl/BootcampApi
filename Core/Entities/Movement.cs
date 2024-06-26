﻿using Core.Constants;

namespace Core.Entities;

public class Movement
{
    public int Id { get; set; }
    public DateTime? TransferredDateTime { get; set; }
    public TransferStatus TransferStatus { get; set; } = TransferStatus.Pending;
    public MovementType MovementType { get; set; } = MovementType.Transference;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;


    public int DestinationAccountId { get; set; }
    public int OriginAccountId { get; set; }


    public virtual Account Account { get; set; } = null!;
}
