using Core.Constants;
using Core.Requests.Customer;
using FluentValidation;

namespace Infrastructure.Validations;

public class UpdateCustomerModelValidation: AbstractValidator<UpdateCustomerModel>
{
    public UpdateCustomerModelValidation()
         {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters");

        RuleFor(x => x.Mail)
                .EmailAddress();

        RuleFor(x => x.Phone)
                .NotNull().WithMessage("Phone cannot be null")
                .NotEmpty().WithMessage("Phone cannot be empty");

        RuleFor(x => x.BankId)
                .NotNull().WithMessage("BankId cannot be null")
                .NotEmpty().WithMessage("BankId cannot be empty");

        RuleFor(x => x.DocumentNumber)
                .NotNull().WithMessage("Document cannot be null")
                .NotEmpty().WithMessage("Document cannot be empty");

        RuleFor(x => x.CustomerStatus)
                 .Must(x => Enum.IsDefined(typeof(CustomerStatus), x))
                 .WithMessage("Invalid Customer Status");
    }
}
