using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;

    public AccountRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<AccountDTO> Add(CreateAccountModel model)
    {
        var customer = await _context.Accounts.FindAsync(model.CustomerId);
        var currency = await _context.Accounts.FindAsync(model.CurrencyId);
        var customer2 = await _context.Customers
        .Include(c => c.Bank)
        .FirstOrDefaultAsync(c => c.Id == model.CustomerId);

        var query = _context.Accounts
       .Include(c => c.SavingAccount)
       .Include(c => c.Customer)
       .Include(c => c.Currency)
       .AsQueryable();
        var result = await query.ToListAsync();

        var accountToCreate = model.Adapt<Account>();
        _context.Accounts.Add(accountToCreate);
        await _context.SaveChangesAsync();


        if (model.Type == "Saving")
        {
            var savingAccountDTO = new CreateSavingAccountDTO
            {
                AccountId = accountToCreate.Id,
                HolderName = accountToCreate.Holder,
                SavingType = model.SavingType
                
            };
            var savingAccount = savingAccountDTO.Adapt<SavingAccount>();
            _context.SavingAccounts.Add(savingAccount);
        }

        else if (model.Type == "Current")
        {
            var currentAccountDTO = new CreateCurrentAccountDTO
            {
                AccountId = accountToCreate.Id,
                OperationalLimit=model.OperationalLimit,
                MonthAverage=model.MonthAverage,
                Interest=model.MonthAverage

            };
            var currentAccount = currentAccountDTO.Adapt<CurrentAccount>();
            _context.CurrentAccounts.Add(currentAccount);
        }
        else
        {
            throw new Exception("Invalid account type");
        }

        await _context.SaveChangesAsync();

        var accountDTO = accountToCreate.Adapt<AccountDTO>();
        return accountDTO;
    }


    public async Task<bool> Delete(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) {throw new NotFoundException($"Account with id: {id} not found");}
        account.IsDeleted = IsDeletedStatus.True;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public Task<AccountDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        var query = _context.Accounts
            .Include(c => c.Customer)
            .ThenInclude(customer => customer.Bank)
            .Include(c => c.Currency)
            .Include(c => c.CurrentAccount)
            .Include(c => c.SavingAccount)
            .AsQueryable();

        if (filter.CurrencyId is not null)
        {
            query = query.Where(x =>
                x.Currency != null &&
                x.CurrencyId == filter.CurrencyId);
        }
        if (filter.Type is not null)
        {
            query = query.Where(a => a.Type.Equals(filter.Type));
        }
        if (filter.Number is not null)
        {
            query = query.Where(x =>
                x.Number != null &&
                x.Number == filter.Number);
        }
        var result = await query.ToListAsync();
        var accountDTO = result.Adapt<List<AccountDTO>>();
        return accountDTO;
    }

    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        var customer = await _context.Accounts.FindAsync(model.CustomerId);
        var currency = await _context.Accounts.FindAsync(model.CurrencyId);
        var customer2 = await _context.Customers
            .Include(c => c.Bank)
            .FirstOrDefaultAsync(c => c.Id == model.CustomerId);
        var query = _context.Accounts
           .Include(c => c.SavingAccount)
           .Include(c => c.CurrentAccount)
           .Include(c => c.Customer)
           .Include(c => c.Currency)
           .AsQueryable();

        var result = await query.ToListAsync();

        var accountToUpdate = await _context.Accounts.FindAsync(model.Id);
        model.Adapt(accountToUpdate);
        if (accountToUpdate is null) throw new NotFoundException($"Account with id: {model.Id} doest not exist");

        _context.Accounts.Update(accountToUpdate);
        await _context.SaveChangesAsync();

        var accountDTO = accountToUpdate.Adapt<AccountDTO>();
        return accountDTO;
    }
}
