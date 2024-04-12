using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class AccountDTO
{
    public int Id { get; set; }
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public AccountType Type { get; set; } = AccountType.Current;
    public decimal Balance { get; set; }
    public AccountStatus Status { get; set; } = AccountStatus.Active;
    public IsDeletedStatus IsDeleted { get; set; } = IsDeletedStatus.False;

    //public int CurrencyId { get; set; }
    public CurrencyDTO Currency { get; set; } = null!;

    //public int CustomerId { get; set; }
    public CustomerDTO Customer { get; set; } = null!;

    public CreateSavingAccountDTO CreateSavingAccountDTO { get; set; } = null!;
    public CreateCurrentAccountDTO CreateCurrentAccountDTO { get; set; } = null!;  
    //public SavingAccount? SavingAccount { get; set; }
    //public CurrentAccount? CurrentAccount { get; set; }
}
