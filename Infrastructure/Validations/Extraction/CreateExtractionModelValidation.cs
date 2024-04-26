using Core.Requests.Extraction;
using FluentValidation;

namespace Infrastructure.Validations.Extraction;

public class CreateExtractionModelValidation : AbstractValidator<CreateExtractionModel>
{
    public CreateExtractionModelValidation()
    {
        {
            RuleFor(x => x.OperationDate)
                .NotNull()
                .WithMessage("Operation date and time cannot be null")
                .NotEmpty().WithMessage("Operation date and time cannot be empty");

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
}
