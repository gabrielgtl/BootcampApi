using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Requests;
using Mapster;

namespace Infrastructure.Mappings;

public class AccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAccountModel, Account>()
            .Map(dest => dest.Holder, src => src.Holder)
            .Map(dest => dest.Number, src => src.Number)
            .Map(dest => dest.Type, src => Enum.Parse<AccountType>(src.Type))
            .Map(dest => dest.CurrencyId, src => src.CurrencyId)
            .Map(dest => dest.CustomerId, src => src.CustomerId);


        config.NewConfig<Account, AccountDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Holder, src => src.Holder)
            .Map(dest => dest.Number, src => src.Number)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.Balance, src => src.Balance)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.IsDeleted, src => src.IsDeleted)

            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.CreateCurrentAccountDTO, src => src.CurrentAccount)
            .Map(dest => dest.CreateSavingAccountDTO, src => src.SavingAccount);
    }
}
