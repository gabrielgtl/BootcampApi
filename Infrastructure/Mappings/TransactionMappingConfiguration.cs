using Core.Entities;
using Core.Models;
using Mapster;

namespace Infrastructure.Mappings
{
    public class TransactionMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Deposit, TransactionsDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.OperationDate, src => src.OperationDate)
                .Map(dest => dest.Description, src => "")
                .Map(dest => dest.Type, src => "Deposit")
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.AccountId, src => src.AccountId);

            config.NewConfig<Extraction, TransactionsDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.OperationDate, src => src.OperationDate)
                .Map(dest => dest.Description, src => "")
                .Map(dest => dest.Type, src => "Extraction")
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.AccountId, src => src.AccountId);

            config.NewConfig<Movement, TransactionsDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.OperationDate, src => src.TransferredDateTime)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Type, src => "Movement")
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.AccountId, src => src.OriginAccountId);

            config.NewConfig<Payment, TransactionsDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Type, src => "Payment")
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.AccountId, src => src.AccountId);

        }
    }
}
