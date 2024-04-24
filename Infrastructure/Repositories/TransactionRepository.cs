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

    public async Task<List<TransactionsDTO>> FilterTransaction(int id, FilterTransactionModel filter)
    {
        var account = await _context.Accounts.FindAsync(id);
        var query = _context.Accounts
            .Include(a => a.Payments)
            .Include(a => a.Extractions)
            .Include(a => a.Deposits)
            .Include(a => a.Movements)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Description))
        {
            var trimmedDescription = filter.Description.Trim().ToLower();
            var escapedDescription = Regex.Escape(trimmedDescription);

            query = query.Where(a =>
                a.Payments.Any(p => p.Description != null && Regex.IsMatch(p.Description.Trim(), escapedDescription, RegexOptions.IgnoreCase)) ||
                a.Movements.Any(m => m.Description != null && Regex.IsMatch(m.Description.Trim(), escapedDescription, RegexOptions.IgnoreCase))

            );
        }

        if (filter.Month != 0 && filter.Year != 0)
        {
            query = query.Where(a =>
                (a.Extractions.Any(e => e.OperationDate.Month == filter.Month && e.OperationDate.Year == filter.Year)) ||
                (a.Deposits.Any(d => d.OperationDate.Month == filter.Month && d.OperationDate.Year == filter.Year)) ||
                (a.Movements.Any(m => m.TransferredDateTime!.Value.Month == filter.Month && m.TransferredDateTime.Value.Year == filter.Year)));
        }

        if (filter.StartDate is not null && filter.EndDate is not null)
        {
            query = query.Where(a =>
                (a.Extractions.Any(e => e.OperationDate.Date >= filter.StartDate && e.OperationDate.Date <= filter.EndDate)) ||
                (a.Deposits.Any(d => d.OperationDate.Date >= filter.StartDate && d.OperationDate.Date <= filter.EndDate)) ||
                (a.Movements.Any(m => m.TransferredDateTime >= filter.StartDate  && m.TransferredDateTime <= filter.EndDate)));
        }
        var result = await query.ToListAsync();
        var transactionDTO = result
            .SelectMany(a => a.Payments.Select(p => p.Adapt<TransactionsDTO>()))
            .Concat(result.SelectMany(a => a.Extractions.Select(e => e.Adapt<TransactionsDTO>())))
            .Concat(result.SelectMany(a => a.Deposits.Select(d => d.Adapt<TransactionsDTO>())))
            .Concat(result.SelectMany(a => a.Movements.Select(m => m.Adapt<TransactionsDTO>())))
            .ToList();


        return transactionDTO;
    }

}
