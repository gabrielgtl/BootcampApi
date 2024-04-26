using Core.Entities;
using Core.Models;
using Core.Requests.Deposit;
using Core.Requests.Extraction;
using Mapster;

namespace Infrastructure.Mappings;

public class ExtractionMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateExtractionModel, Extraction>()
                    .Map(dest => dest.Amount, src => src.Amount)
                    .Map(dest => dest.OperationDate, src => src.OperationDate)
                    .Map(dest => dest.Description, src => $"DestinationBank:{src.BankId}, {src.Description}")
                    .Map(dest => dest.Amount, src => src.Amount)
                    .Map(dest => dest.AccountId, src => src.AccountId);

        config.NewConfig<CreateExtractionModel, Account>()
            .AfterMapping((src, dest) =>
            {
                if (dest.Id == src.AccountId)
                {
                    dest.Balance -= src.Amount;
                }
            });

        config.NewConfig<Extraction, ExtractionDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.OperationDate, src => src.OperationDate)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.HolderName, src => src.Account.Holder)
            .Map(dest => dest.HolderName, src => src.Account.Customer.Bank.Name);

    }
}
