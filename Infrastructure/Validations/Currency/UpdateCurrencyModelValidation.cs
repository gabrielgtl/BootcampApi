﻿using Core.Requests.Currency;
using FluentValidation;

namespace Infrastructure.Validations.Currency
{
    public class UpdateCurrencyModelValidation : AbstractValidator<UpdateCurrencyModel>
    {
        public UpdateCurrencyModelValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name cannot be empty");

            RuleFor(x => x.SellValue)
                .NotNull().WithMessage("Sell Value cannot be null")
                .NotEmpty().WithMessage("Sell Value cannot be empty");

            RuleFor(x => x.BuyValue)
                .NotNull().WithMessage("Buy Value cannot be null")
                .NotEmpty().WithMessage("Buy Value cannot be empty");
        }
    }
}
