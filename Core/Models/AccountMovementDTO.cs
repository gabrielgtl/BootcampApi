using Core.Constants;

namespace Core.Models;

public class AccountMovementDTO
{
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public AccountType Type { get; set; } = AccountType.Current;
    public decimal Balance;
}
