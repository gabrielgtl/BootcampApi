using Core.Requests.Movements;
using FluentValidation;

namespace Infrastructure.Validations.Movemen;
public class CreateMovementModelValidation : AbstractValidator<CreateMovementModel>
{
    public CreateMovementModelValidation()
    {
        RuleFor(x => x.TransferredDateTime)
                    .NotNull()
                    .WithMessage("Transferred date and time cannot be null");

        RuleFor(x => x.TransferStatus)
                    .IsInEnum()
                    .WithMessage("Invalid transfer status");

        RuleFor(x => x.MovementType)
                    .IsInEnum()
                    .WithMessage("Invalid movement type");

        RuleFor(x => x.Amount)
                    .GreaterThan(0)
                    .WithMessage("Amount must be greater than zero");

        RuleFor(x => x.OriginAccountId)
                    .GreaterThan(0)
                    .WithMessage("Origin account ID must be greater than zero");

        RuleFor(x => x.DestinationAccountId)
                    .GreaterThan(0)
                    .WithMessage("Destination account ID must be greater than zero");

        RuleFor(m => new { m.OriginAccountId, m.DestinationAccountId })
                    .Must(x => ValidAccountId(x.OriginAccountId, x.DestinationAccountId))
                    .WithMessage("Invalid account combination");
    }

    private bool ValidAccountId(int originAccountId, int destinationAccountId)
    {
        return originAccountId != destinationAccountId;
    }
}
