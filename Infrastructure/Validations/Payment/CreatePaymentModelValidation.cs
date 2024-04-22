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
                        .GreaterThan(0)
                        .WithMessage("Amount must be greater than zero");

            RuleFor(x => x.AccountId)
                        .GreaterThan(0)
                        .WithMessage("AccountId must be greater than zero");

        }
    }
}
