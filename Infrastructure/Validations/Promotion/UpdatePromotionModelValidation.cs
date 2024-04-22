using Core.Requests.Promotion;
using FluentValidation;

namespace Infrastructure.Validations.Promotion;

public class UpdatePromotionModelValidation : AbstractValidator<UpdatePromotionModel>
{
    public UpdatePromotionModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");

    }
}
