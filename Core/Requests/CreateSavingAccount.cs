using Core.Constants;
using Core.Entities;

namespace Core.Requests;

public class CreateSavingAccount
{
    public SavingType SavingType { get; set; }
}
