using Core.Requests.Payment;
using FluentValidation;

namespace Infrastructure.Validations.Payment
{
    public class CreatePaymentModelValidation : AbstractValidator<CreatePaymentModel>
    {
        public CreatePaymentModelValidation() 
        {
            RuleFor(x => x.Description)
                        .NotNull()
                        .WithMessage("Transferred date and time cannot be null");

            RuleFor(x => x.Amount)
                        .NotNull().WithMessage("Amount must not be null")
                        .GreaterThan(0)
                        .WithMessage("Amount must be greater than zero");

            RuleFor(x => x.AccountId)
                        .NotNull().WithMessage("AccountId must not be null")
                        .GreaterThan(0)
                        .WithMessage("AccountId must be greater than zero");

        }
    }
}
