using Core.Constants;
using Core.Requests.CreditCard;
using FluentValidation;
namespace Infrastructure.Validations.CreditCard;

public class UpdateCreditCardValidation : AbstractValidator<UpdateCreditCardModel>
{
    public UpdateCreditCardValidation()
    {
        RuleFor(x => x.Designation)
           .NotNull().WithMessage("Designation cannot be null")
           .NotEmpty().WithMessage("Designation cannot be empty")
           .MinimumLength(5).WithMessage("Designation must have at least 5 characters");

        RuleFor(x => x.CreditCardStatus)
            .Must(x => Enum.IsDefined(typeof(CreditCardStatus), x))
            .WithMessage("Invalid Customer Status");

        RuleFor(x => x.IssueDate)
            .NotNull().WithMessage("Issue Date cannot be null")
            .NotEmpty().WithMessage("Issue Date cannot be empty");

        RuleFor(x => x.ExpirationDate)
            .NotNull().WithMessage("Expiration Date cannot be null")
            .NotEmpty().WithMessage("Expiration Date cannot be empty");

        RuleFor(x => x.CardNumber)
            .NotNull().WithMessage("Card Number cannot be null")
            .NotEmpty().WithMessage("Card Number cannot be empty")
            .MinimumLength(16).WithMessage("Card Number must have at least 16 characters");

        RuleFor(x => x.Cvv)
            .NotNull().WithMessage("CVV cannot be null")
            .NotEmpty().WithMessage("CVV cannot be empty")
            .Must(w => w.ToString().Length < 4).WithMessage("CVV cannot be larger than 3 digits")
            .Must(w => w.ToString().Length > 2).WithMessage("CVV cannot be smaller than 3 digits");

        RuleFor(x => x.CreditLimit)
            .NotNull().WithMessage("Credit Limit cannot be null")
            .NotEmpty().WithMessage("Credit Limit cannot be empty");

        RuleFor(x => x.InterestRate)
            .NotNull().WithMessage("Interest Rate cannot be null")
            .GreaterThan(0).WithMessage("Interest must be greater than zero.");

        RuleFor(x => x.CustomerId)
            .NotNull().WithMessage("CustomerId cannot be null")
            .NotEmpty().WithMessage("CustomerId cannot be empty");

        RuleFor(x => x.CurrencyId)
            .NotNull().WithMessage("CurrencyId cannot be null")
            .NotEmpty().WithMessage("CurrencyId cannot be empty");

        RuleFor(x => x.AvailableCredit)
            .NotNull().WithMessage("Available Credit cannot be null")
            .GreaterThan(500000).WithMessage("Available Credit must be greater than five hundred thousand.");
    }
}
