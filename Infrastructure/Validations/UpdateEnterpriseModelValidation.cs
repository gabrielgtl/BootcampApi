using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;

public class UpdateEnterpriseModelValidation : AbstractValidator<UpdateEnterpriseModel>
{
    public UpdateEnterpriseModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");

        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Phone)
            .NotNull().WithMessage("Phone cannot be null")
            .NotEmpty().WithMessage("Phone cannot be empty");
    }
}
