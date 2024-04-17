using Core.Constants;
using Core.Requests.Customer;
using Core.Requests.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class UpdateRequestModelValidation : AbstractValidator<UpdateRequestModel>
{
    public UpdateRequestModelValidation()
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

        When(x => x.ProductId == 2 /* ID del producto Tarjeta */, () => {
            RuleFor(x => x.Brand)
                .NotNull().WithMessage("Brand cannot be null")
                .NotEmpty().WithMessage("Brand cannot be empty");
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0")
                .NotNull().WithMessage("Amount cannot be null");
        });

        When(x => x.ProductId == 1 /* ID del producto Credit */, () => {
            RuleFor(x => x.Term)
                .GreaterThan(0).WithMessage("Term must be greater than 0")
                .NotNull().WithMessage("Term cannot be null");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0")
                .NotNull().WithMessage("Amount cannot be null");
        });
        When(x => x.ProductId == 3 /* ID del producto SavingAccount */, () => {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0")
                .NotNull().WithMessage("Amount cannot be null");
        });
    }
}
