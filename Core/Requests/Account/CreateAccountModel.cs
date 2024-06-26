﻿using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Requests.CurrentAccount;
using Core.Requests.SavingAccount;

namespace Core.Requests.Account;

public class CreateAccountModel
{
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }
    public CreateSavingAccount? CreateSavingAccount { get; set; }
    public CreateCurrentAccount? CreateCurrentAccount { get; set; }

}
