using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Requests.CreditCard;
using Mapster;

namespace Infrastructure.Mappings;

public class CreditCardMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config) 
    {
        config.NewConfig<CreateCreditCardModel, CreditCard>()
           .Map(dest => dest.Designation, src => src.Designation)
           .Map(dest => dest.IssueDate, src => src.IssueDate)
           .Map(dest => dest.ExpirationDate, src => src.ExpirationDate)
           .Map(dest => dest.CardNumber, src => src.CardNumber)
           .Map(dest => dest.Cvv, src => src.Cvv)
           .Map(dest => dest.CreditCardStatus, src => Enum.Parse<CreditCardStatus>(src.CreditCardStatus))
           .Map(dest => dest.CreditLimit, src => src.CreditLimit)
           .Map(dest => dest.AvailableCredit, src => src.AvailableCredit)
           .Map(dest => dest.CurrentDebt, src => src.CurrentDebt)
           .Map(dest => dest.InterestRate, src => src.InterestRate)

           .Map(dest => dest.ClientId, src => src.CustomerId)
           .Map(dest => dest.CurrencyId, src => src.CurrencyId);

        config.NewConfig<CreditCard, CreditCardDTO>()
           .Map(dest => dest.Id, src => src.Id)
           .Map(dest => dest.Designation, src => src.Designation)
           .Map(dest => dest.IssueDate, src => src.IssueDate)
           .Map(dest => dest.ExpirationDate, src => src.ExpirationDate)
           .Map(dest => dest.CardNumber, src => src.CardNumber)
           .Map(dest => dest.Cvv, src => src.Cvv)
           .Map(dest => dest.CreditCardStatus, src => src.CreditCardStatus)
           .Map(dest => dest.CreditLimit, src => src.CreditLimit)
           .Map(dest => dest.AvailableCredit, src => src.AvailableCredit)
           .Map(dest => dest.CurrentDebt, src => src.CurrentDebt)
           .Map(dest => dest.InterestRate, src => src.InterestRate)
           .Map(dest => dest.RestrictedCreditCard, src => $"xxxx-xxxx-xxxx-{src.CardNumber.Substring(src.CardNumber.Length - 4)}")

           .Map(dest => dest.Customer, src => src.Customer)
           .Map(dest => dest.Currency, src => src.Currency);

    }
}
