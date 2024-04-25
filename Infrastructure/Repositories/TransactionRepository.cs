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

        // Aplicar filtro de fecha de inicio si está especificado
        if (filter.StartDate.HasValue)
        {
            query = query.Where(a =>
                a.Movements.Any(m =>
                    m.TransferredDateTime >= filter.StartDate.Value.Date ||
                    a.Deposits.Any(d => d.OperationDate >= filter.StartDate.Value) ||
                    a.Extractions.Any(w => w.OperationDate >= filter.StartDate.Value)
                )
            );
        }

        // Aplicar filtro de fecha de fin si está especificado
        if (filter.EndDate.HasValue)
        {
            query = query.Where(a =>
                a.Movements.Any(m =>
                    m.TransferredDateTime <= filter.EndDate.Value.Date ||
                    a.Deposits.Any(d => d.OperationDate >= filter.EndDate.Value) ||
                    a.Extractions.Any(w => w.OperationDate >= filter.EndDate.Value)
                )
            );
        }
        var result = await query.ToListAsync();

        var accountTransactionsDTOs = result.SelectMany(a => a.Movements.Select(m => m.Adapt<TransactionsDTO>())
                        .Concat(a.Deposits.Select(d => d.Adapt<TransactionsDTO>()))
                        .Concat(a.Extractions.Select(w => w.Adapt<TransactionsDTO>()))
                        .Concat(a.Payments.Select(p => p.Adapt<TransactionsDTO>())))
                        .ToList();
        return accountTransactionsDTOs;
    }

}
