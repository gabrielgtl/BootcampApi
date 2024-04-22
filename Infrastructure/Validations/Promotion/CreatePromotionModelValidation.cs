using Core.Requests.Promotion;
using FluentValidation;

namespace Infrastructure.Validations.Promotion;

public class CreatePromotionModelValidation : AbstractValidator<CreatePromotionModel>
{
    public CreatePromotionModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");

        RuleFor(x => x.Enterprises)
            .NotNull().WithMessage("Enterprises cannot be null")
            .NotEmpty().WithMessage("Enterprises cannot be empty");

    }
}
