using Core.Constants;
using Core.Requests.Bank;
using Core.Requests.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateRequestModelValidation : AbstractValidator<CreateRequestModel>
{
    public CreateRequestModelValidation()
    {
        RuleFor(x => x.RequestStatus)
            .Must(x => Enum.IsDefined(typeof(RequestStatus), x))
            .WithMessage("Invalid Request Status");

        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("ProductId cannot be null")
            .NotEmpty().WithMessage("ProductId cannot be empty");

        RuleFor(x => x.CurrencyId)
            .NotNull().WithMessage("CurrencyId cannot be null")
            .NotEmpty().WithMessage("CurrencyId cannot be empty");

        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("ProductId cannot be null")
            .NotEmpty().WithMessage("ProductId cannot be empty");
    }
}
