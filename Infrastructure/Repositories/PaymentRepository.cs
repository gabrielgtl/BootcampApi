using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Payment;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly BootcampContext _context;

    public PaymentRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<(bool isValid, string message)> DataValidation(CreatePaymentModel model)
    {
        var originAccount = await _context.Accounts
             .Include(m => m.CurrentAccount)
             .Include(m => m.SavingAccount)
             .Include(m => m.Customer)
             .Where(m => m.Id == model.AccountId)
             .FirstOrDefaultAsync();
      
        if (originAccount == null) { return (false, "Account does not exist"); }

        if (originAccount.Customer.DocumentNumber != model.DocumentNumber) { return (false, "Wrong Documnet Number"); }
        if (originAccount.Balance < model.Amount) { return (false, "You don't have that much money"); }

        return (true, "Validations Passed");
    }


    public async Task<PaymentDTO> Payment(CreatePaymentModel model)
    {
        var payment = model.Adapt<Payment>();
        var service = await _context.Services.FindAsync(model.ServiceId);
        if (service == null)
            throw new NotFoundException($"The service with id: {model.ServiceId} does not exist");

        _context.Payments.Add(payment);


        var destinationAccount = _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .FirstOrDefault(a => a.Id == model.AccountId);

        if (destinationAccount != null)
        {
            var mappedDestinationAccount = model.Adapt(destinationAccount);
            _context.Entry(mappedDestinationAccount).State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();
        var createdPayment = await _context.Payments
            .Include(a => a.Account)
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == payment.Id);

        return createdPayment.Adapt<PaymentDTO>();
    }
}
