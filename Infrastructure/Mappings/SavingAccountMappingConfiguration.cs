using Core.Entities;
using Core.Models;
using Mapster;

namespace Infrastructure.Mappings;

public class SavingAccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SavingAccount, CreateSavingAccountDTO>()
           .Map(dest => dest.Id, src => src.Id)
           .Map(dest => dest.SavingType, src => src.SavingType)
           .Map(dest => dest.HolderName, src => src.HolderName)
           .Map(dest => dest.AccountId, src => src.AccountId);
    }
}
