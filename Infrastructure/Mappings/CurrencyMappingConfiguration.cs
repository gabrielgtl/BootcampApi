using Core.Entities;
using Core.Models;
using Core.Requests.Currency;
using Mapster;

namespace Infrastructure.Mappings;

public class CurrencyMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCurrencyModel, Currency>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.BuyValue, src => src.BuyValue)
            .Map(dest => dest.SellValue, src => src.SellValue);

        config.NewConfig<Currency, CurrencyDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.BuyValue, src => src.BuyValue)
            .Map(dest => dest.SellValue, src => src.SellValue);
    }
}
