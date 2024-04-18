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
    .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime.HasValue ? DateTime.SpecifyKind(src.TransferredDateTime.Value, DateTimeKind.Unspecified) : default(DateTime?)).Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src 
            => $"{src.DestinyBankId}, {src.AccountNumber}, {src.DocumentNumber}," +
            $" {src.CurrencyId}, {src.DestinationAccountId}, {src.OriginAccountId}");


        config.NewConfig<Movement, MovementDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AccountDestiny, src => src.DestinationAccountId)
            .Map(dest => dest.AccountSource, src => src.OriginAccountId);
    }
}
