using Core.Requests.Deposit;
using FluentValidation;

namespace Infrastructure.Validations.Deposit;

public class CreateDepositModelValidation : AbstractValidator<CreateDepositModel>
{
    public CreateDepositModelValidation()
    {
        RuleFor(x => x.OperationDate)
            .NotNull()
            .WithMessage("Operation date and time cannot be null");

        RuleFor(x => x.Amount)
                    .NotNull().WithMessage("Amount cannot be null")
                    .GreaterThan(0)
                    .WithMessage("Amount must be greater than zero");

        RuleFor(x => x.AccountId)
                    .NotNull().WithMessage("AccountId cannot be null")
                    .GreaterThan(0)
                    .WithMessage("AccountId must be greater than zero");
    }
}
