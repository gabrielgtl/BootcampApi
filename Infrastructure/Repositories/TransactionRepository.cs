using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Transaction;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BootcampContext _context;

    public TransactionRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<List<TransactionsDTO>> FilterTransaction(FilterTransactionModel filter)
    {
        var payments = await _context.Payments.Where(p => p.AccountId == filter.AccountId).ToListAsync();
        var extractions = await _context.Extractions.Where(e => e.AccountId == filter.AccountId).ToListAsync();
        var deposits = await _context.Deposits.Where(d => d.AccountId == filter.AccountId).ToListAsync();
        var movements = await _context.Movements.Where(m => m.OriginAccountId == filter.AccountId).ToListAsync();


        var filteredPayments = payments.Where(p =>
            string.IsNullOrEmpty(filter.Description) ||
            (p.Description != null && Regex.IsMatch(p.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))
        ).ToList();

        var filteredExtractions = extractions.Where(e =>
            (filter.Month == 0 || filter.Year == 0 || (e.OperationDate.Month == filter.Month && e.OperationDate.Year == filter.Year)) &&
            (!filter.StartDate.HasValue || e.OperationDate >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || e.OperationDate <= filter.EndDate.Value.Date)
        ).ToList();

        var filteredDeposits = deposits.Where(d =>
            (filter.Month == 0 || filter.Year == 0 || (d.OperationDate.Month == filter.Month && d.OperationDate.Year == filter.Year)) &&
            (!filter.StartDate.HasValue || d.OperationDate >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || d.OperationDate <= filter.EndDate.Value.Date)
        ).ToList();

        var filteredMovements = movements.Where(m =>
            (string.IsNullOrEmpty(filter.Description) || (m.Description != null && Regex.IsMatch(m.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))) &&
            (filter.Month == 0 || filter.Year == 0 || (m.TransferredDateTime!.Value.Month == filter.Month && m.TransferredDateTime.Value.Year == filter.Year)) &&
            (!filter.StartDate.HasValue || m.TransferredDateTime >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || m.TransferredDateTime <= filter.EndDate.Value.Date)
        ).ToList();

        var combinedResults = new List<TransactionsDTO>();

        if (filteredPayments.Any())
            combinedResults.AddRange(filteredPayments.Select(p => p.Adapt<TransactionsDTO>()));

        if (filteredExtractions.Any())
            combinedResults.AddRange(filteredExtractions.Select(e => e.Adapt<TransactionsDTO>()));

        if (filteredDeposits.Any())
            combinedResults.AddRange(filteredDeposits.Select(d => d.Adapt<TransactionsDTO>()));

        if (filteredMovements.Any())
            combinedResults.AddRange(filteredMovements.Select(m => m.Adapt<TransactionsDTO>()));
        if (filter.Type != null)

        {

            combinedResults = combinedResults.Where(t => t.Type == filter.Type).ToList();

        }
        return combinedResults;
    }


}
