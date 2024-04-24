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

        var transactionDate = model.OperationDate;

        decimal totalExtractionsAmount = await _context.Extractions
            .Where(e => e.OperationDate.Month == transactionDate.Month)
            .SumAsync(e => e.Amount);

        decimal totalDepositsAmount = await _context.Deposits
            .Where(d => d.OperationDate.Month == transactionDate.Month)
            .SumAsync(d => d.Amount);

        decimal totalMovementsAmount = await _context.Movements
            .Where(m => m.TransferredDateTime!.Value.Month == transactionDate.Month)
            .SumAsync(m => m.Amount);

        decimal totalTransactionsAmount =
            totalExtractionsAmount + totalDepositsAmount + totalMovementsAmount + model.Amount;

        if (originAccount is null) { return (false, "Account does not exist"); }

        if (originAccount.Type == AccountType.Current)
        {
            var currentAccount = originAccount.CurrentAccount;
            if (currentAccount != null && (model.Amount > currentAccount.OperationalLimit
                || totalTransactionsAmount > currentAccount.OperationalLimit))
            {
                return (false, "Transaction Operation limit exceeded.");
            }
        }

        if (originAccount.Customer.BankId != model.BankId)
        {
            return (false, "The destination bank does not match the entered bank.");
        }
        return (true, "Validations Passed");
    }

    public Task<ExtractionDTO> Extraction(CreateExtractionModel model)
    {
        throw new NotImplementedException();
    }
}
