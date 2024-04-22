using Core.Constants;
using Core.Entities;
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
    public async Task<PaymentDTO> Payment(CreatePaymentModel model)
    {
        var payment = model.Adapt<Payment>();

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
