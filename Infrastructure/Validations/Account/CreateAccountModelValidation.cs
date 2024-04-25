using Core.Constants;
using Core.Requests.Account;
using FluentValidation;

namespace Infrastructure.Validations.Account;

public class CreateAccountModelValidation : AbstractValidator<CreateAccountModel>
{
    public CreateAccountModelValidation()
    {
        RuleFor(x => x.Holder)
            .NotNull().WithMessage("Holder cannot be null")
            .NotEmpty().WithMessage("Holder cannot be empty")
            .MaximumLength(100).WithMessage("Holder cannot be longer than 100 characters");
        RuleFor(x => x.Number)
            .NotNull().WithMessage("Number cannot be null")
            .NotEmpty().WithMessage("Number cannot be empty")
            .MaximumLength(50).WithMessage("Number cannot be longer than 50 characters");
        RuleFor(x => x.AccountType)
            .Must(x => Enum.IsDefined(typeof(AccountType), x))
            .WithMessage("Invalid Account Type");
 
        RuleFor(x => x.CurrencyId)
            .NotEmpty().WithMessage("Currency cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Currency");
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer cannot be empty")
            .Must(x => x > 0).WithMessage("Invalid Customer");
    }
}
