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

    public Task<(bool isValid, string message)> DataValidation(CreateExtractionModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<ExtractionDTO> Extraction(CreateExtractionModel model)
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
        var createdExtractions = await _context.Extractions
            .Include(a => a.Account)
            .FirstOrDefaultAsync(a => a.Id == extraction.Id);

        return createdExtractions.Adapt<ExtractionDTO>();
    }
}
