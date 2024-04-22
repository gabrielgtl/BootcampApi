using Core.Entities;
using Core.Models;
using Core.Requests;
using Mapster;

namespace Infrastructure.Mappings;

public class PaymentMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePaymentModel, Payment>()
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => $"DocumentNumber:{src.DocumentNumber}, {src.Description}")
            .Map(dest => dest.AccountId, src => src.AccountId)
            .Map(dest => dest.ServiceId, src => src.ServiceId);

        config.NewConfig<CreatePaymentModel, Account>()
            .AfterMapping((src, dest) =>
            {
                if (dest.Id == src.AccountId)
                {
                    dest.Balance -= src.Amount;
                    if (dest.CurrentAccount != null)
                    {
                        dest.CurrentAccount.OperationalLimit -= src.Amount;
                    }
                }
            });

        config.NewConfig<Payment, PaymentDTO>()
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Service, src => src.Service.Name)
            .Map(dest => dest.AccountHolder, src => src.Account.Holder);
    }
}
