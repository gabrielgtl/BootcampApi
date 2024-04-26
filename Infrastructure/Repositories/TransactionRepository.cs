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
        var payments = await _context.Payments
            .Where(p => p.AccountId == filter.AccountId).ToListAsync();

        var extractions = await _context.Extractions
            .Where(e => e.AccountId == filter.AccountId).ToListAsync();

        var deposits = await _context.Deposits
            .Where(d => d.AccountId == filter.AccountId).ToListAsync();

        var movements = await _context.Movements
            .Where(m =>(m.OriginAccountId == filter.AccountId || m.DestinationAccountId == filter.AccountId)
        ).ToListAsync();

        var filteredPayments = payments.Where(p =>
            (string.IsNullOrEmpty(filter.Description) ||
            (p.Description != null && Regex.IsMatch(p.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))) &&
            (!filter.StartDate.HasValue || p.OperationDate.Date >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || p.OperationDate.Date <= filter.EndDate.Value.Date) &&
            (filter.Month == 0 || p.OperationDate.Month == filter.Month) &&
            (filter.Year == 0 || p.OperationDate.Year == filter.Year)
        ).ToList();      


        var filteredExtractions = extractions.Where(e =>
            (string.IsNullOrEmpty(filter.Description) ||
            (e.Description != null && Regex.IsMatch(e.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))) &&
            (!filter.StartDate.HasValue || e.OperationDate.Date >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || e.OperationDate.Date <= filter.EndDate.Value.Date) &&
            (filter.Month == 0 || e.OperationDate.Month == filter.Month) &&
            (filter.Year == 0 || e.OperationDate.Year == filter.Year)
        ).ToList();

        var filteredDeposits = deposits.Where(d =>
            (string.IsNullOrEmpty(filter.Description) ||
            (d.Description != null && Regex.IsMatch(d.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))) &&
            (!filter.StartDate.HasValue || d.OperationDate.Date >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || d.OperationDate.Date <= filter.EndDate.Value.Date) &&
            (filter.Month == 0 || d.OperationDate.Month == filter.Month) &&
            (filter.Year == 0 || d.OperationDate.Year == filter.Year)
        ).ToList();


        var filteredMovements = movements.Where(m =>
            (string.IsNullOrEmpty(filter.Description) ||
            (m.Description != null && Regex.IsMatch(m.Description.Trim(), filter.Description.Trim(), RegexOptions.IgnoreCase))) &&
            (!filter.StartDate.HasValue || m.TransferredDateTime!.Value.Date >= filter.StartDate.Value.Date) &&
            (!filter.EndDate.HasValue || m.TransferredDateTime!.Value.Date <= filter.EndDate.Value.Date) &&
            (filter.Month == 0 || m.TransferredDateTime!.Value.Month == filter.Month) &&
            (filter.Year == 0 || m.TransferredDateTime!.Value.Year == filter.Year)
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
