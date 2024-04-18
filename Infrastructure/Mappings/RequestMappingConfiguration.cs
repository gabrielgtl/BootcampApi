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
            .Map(dest => dest.RequestStatus, src => src.RequestStatus)
            .Map(dest => dest.DateOfSolicitation, src => src.DateOfSolicitation)
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.CurrencyId, src => src.CurrencyId);

        config.NewConfig<Request, RequestDTO>()
            .Map(dest => dest.RequestStatus, src => src.RequestStatus.ToString())
            .Map(dest => dest.DateOfApproval, src => src.DateOfApproval)
            .Map(dest => dest.DateOfSolicitation, src => src.DateOfSolicitation)
            .Map(dest => dest.CustomerName, src => src.Customer.Name)
            .Map(dest => dest.ProductName, src => src.Product.Name)
            .Map(dest => dest.CurrencyName, src => src.Currency.Name);

    }
}
