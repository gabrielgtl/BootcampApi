using Core.Entities;
using Core.Models;
using Core.Requests.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class RequestMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRequestModel, Request>()
            .Map(dest => dest.Brand, src => src.Brand)
            .Map(dest => dest.Term, src => src.Term)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.RequestStatus, src => src.RequestStatus)
            .Map(dest => dest.DateOfApproval, src => src.DateOfApproval)
            .Map(dest => dest.DateOfSolicitation, src => src.DateOfSolicitation)
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId);

        config.NewConfig<Request, RequestDTO>()
            .Map(dest => dest.Brand, src => src.Brand)
            .Map(dest => dest.Term, src => src.Term)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.RequestStatus, src => src.RequestStatus)
            .Map(dest => dest.DateOfApproval, src => src.DateOfApproval)
            .Map(dest => dest.DateOfSolicitation, src => src.DateOfSolicitation)
            .Map(dest => dest.ProductDTO, src => src.Product)
            .Map(dest => dest.CurrencyDTO, src => src.Currency);

    }
}
