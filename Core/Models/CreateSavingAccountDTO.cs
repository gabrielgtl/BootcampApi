using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class CreateSavingAccountDTO
{
    public int Id { get; set; }

    public SavingType SavingType { get; set; } = SavingType.Insight;

    public string HolderName { get; set; } = string.Empty;
    public int AccountId { get; set; }
}
