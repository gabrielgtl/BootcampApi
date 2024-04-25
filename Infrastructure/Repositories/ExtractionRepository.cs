using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Extraction;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExtractionRepository : IExtractionRepository
{
    private readonly BootcampContext _context;

    public ExtractionRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<(bool isValid, string message)> DataValidation(CreateExtractionModel model)
    {
        var originAccount = await _context.Accounts
            .Include(m => m.CurrentAccount)
            .Include(m => m.SavingAccount)
            .Include(m => m.Customer)
            .Where(m => m.Id == model.AccountId)
            .FirstOrDefaultAsync();



        if (originAccount is null) { return (false, "Account does not exist"); }

        if (originAccount.Customer.BankId != model.BankId)
        {
            return (false, "The destination bank does not match the entered bank.");
        }

        if (originAccount.Balance < model.Amount)
        {
            return (false, "You don't have that much money");
        }
        return (true, "Validations Passed");
    }

    public async Task<ExtractionDTO> Extraction(CreateExtractionModel model)
    {
        {
            var extraction = model.Adapt<Extraction>();

            _context.Extractions.Add(extraction);


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
            var createdExtraction = await _context.Extractions
                .Include(a => a.Account)
                .FirstOrDefaultAsync(a => a.Id == extraction.Id);

            return createdExtraction.Adapt<ExtractionDTO>();
        }
    }
}
