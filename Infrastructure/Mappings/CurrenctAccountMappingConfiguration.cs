using Core.Entities;
using Core.Models;
using Mapster;

namespace Infrastructure.Mappings;

public class CurrenctAccountMappingConfiguration
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CurrentAccount, CreateCurrentAccountDTO>()
           .Map(dest => dest.Id, src => src.Id)
           .Map(dest => dest.OperationalLimit, src => src.OperationalLimit)
           .Map(dest => dest.MonthAverage, src => src.MonthAverage)
           .Map(dest => dest.Interest, src => src.Interest)
           .Map(dest => dest.AccountId, src => src.AccountId);
    }
}
