using Core.Entities;
using Core.Models;
using Core.Requests.Movements;
using Mapster;

namespace Infrastructure.Mappings;

public class MovementMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMovementModel, Movement>()
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src
            => $"DestinationBank:{src.DestinyBankId}, AccountNumber:{src.AccountNumber}, DocumentNumber:{src.DocumentNumber}," +
            $" CurrencyId:{src.CurrencyId}, DestinationAccountId:{src.DestinationAccountId}, OriginAccountId:{src.OriginAccountId}");

        config.NewConfig<CreateMovementModel, Account>()
            .AfterMapping((src, dest) =>
            {
                if (dest.Id == src.OriginAccountId)
                {
                    dest.Balance -= src.Amount;
                    if (dest.CurrentAccount != null)
                    {
                        dest.CurrentAccount.OperationalLimit -= src.Amount;
                    }
                    /* else if (dest.SavingAccount != null)
                     {
                         dest.SavingAccount.OperationalLimit -= src.Amount;
                     }*/
                }
                else if (dest.Id == src.DestinationAccountId)
                {
                    dest.Balance += src.Amount;
                    if (dest.CurrentAccount != null)
                    {
                        dest.CurrentAccount.OperationalLimit -= src.Amount;
                    }
                    /* else if (dest.SavingAccount != null)
                     {
                         dest.SavingAccount.OperationalLimit += src.Amount;
                     }*/
                }
            });



        config.NewConfig<Movement, MovementDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AccountDestiny, src => src.DestinationAccountId)
            .Map(dest => dest.AccountSource, src => src.OriginAccountId)
            .AfterMapping((src, dest) =>
            {
                if (dest.AccountSource == src.Account.Id)
                {
                    dest.BalanceOriginAcount = src.Account.Balance;
                }
                else if (dest.AccountDestiny == src.Account.Id)
                {
                    dest.BalanceOriginAcount = src.Account.Balance;
                }
            });
    }
}
