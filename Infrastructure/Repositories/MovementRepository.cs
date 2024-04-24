using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Movements;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly BootcampContext _context;

    public MovementRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<MovementDTO> Transference(CreateMovementModel model)
    {
        var movement = model.Adapt<Movement>();

        _context.Movements.Add(movement);

        var originAccount = _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .FirstOrDefault(a => a.Id == model.OriginAccountId);

        var destinationAccount = _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .FirstOrDefault(a => a.Id == model.DestinationAccountId);


        if (originAccount != null && destinationAccount != null)
        {
            var mappedOriginAccount = model.Adapt(originAccount);
            var mappedDestinationAccount = model.Adapt(destinationAccount);

            _context.Entry(mappedOriginAccount).State = EntityState.Modified;
            _context.Entry(mappedDestinationAccount).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();

        var createdMovement = await _context.Movements
            .Include(a => a.Account)
            .FirstOrDefaultAsync(a => a.Id == movement.Id);

        return createdMovement.Adapt<MovementDTO>();
    }


    public async Task<MovementDTO> GetById(int id)
    {
        var movement = await _context.Movements
            .Include(a => a.Account)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (movement is null) throw new NotFoundException($"The account with id: {id} doest not exist");

        return movement.Adapt<MovementDTO>();
    }
    public async Task<(bool isValid, string message)> DataValidation(CreateMovementModel model)
    {
        var originMovement = await _context.Movements
            .Include(m => m.Account)
            .ThenInclude(m=>m.CurrentAccount)
            .Include(m => m.Account)
            .ThenInclude(m => m.SavingAccount)
            .Include(m => m.Account)
            .ThenInclude(m => m.Customer)
            .Where(m => m.OriginAccountId == model.OriginAccountId)
            .FirstOrDefaultAsync();

        var destinationMovement = await _context.Movements
            .Include(m => m.Account)
            .ThenInclude(m => m.CurrentAccount)
            .Include(m => m.Account)
            .ThenInclude(m => m.SavingAccount)
            .Include(m => m.Account)
            .ThenInclude(m => m.Customer)
            .ThenInclude(m=> m.Bank)
            .Where(m => m.DestinationAccountId == model.DestinationAccountId)
            .FirstOrDefaultAsync();

        if (originMovement == null || destinationMovement == null)
        {
            return (false, "Account does not exist");
        }
        
        if (originMovement.Account.CurrencyId != destinationMovement.Account.CurrencyId)
        {
            return (false, "Account with different currency");
        }

        if (originMovement.Account.Type != destinationMovement.Account.Type)
        {
            return (false, "Account with different account type");
        }

        if (model.Amount > originMovement.Account.Balance)
        {
            return (false, "Amount is bigger than current balance");
        }

        if (originMovement.Account.Status != AccountStatus.Active 
            && destinationMovement.Account.Status != AccountStatus.Active)
        {
            return (false, "Account status is not active");
        }

        if (destinationMovement.Account.Number != model.AccountNumber)
        {
            return (false, "Account number not valid");
        }

        if (destinationMovement.Account.Customer.DocumentNumber != model.DocumentNumber)
        {
            return (false, "Customer document number not valid");
        }
        if (originMovement.Account.Customer.Bank.Id == destinationMovement.Account.Customer.Bank.Id)
        {
            if (string.IsNullOrEmpty(model.DocumentNumber) || string.IsNullOrEmpty(model.AccountNumber) || model.DestinyBankId == 0)
            {
                return (false, "Document number, account number, and destiny bank ID are required when transferring within the same bank.");
            }
        }
        var transactionDate = model.TransferredDateTime;

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
        if (originMovement.Account.Type == AccountType.Current)
        {
            var currentAccount = originMovement.Account.CurrentAccount;
            if (currentAccount != null && (model.Amount > currentAccount.OperationalLimit
                || totalTransactionsAmount > currentAccount.OperationalLimit))
            {
                return (false, "Transaction Operation limit exceeded.");
            }
        }
        if (destinationMovement.Account.Type == AccountType.Current)
        {
            var currentAccount = destinationMovement.Account.CurrentAccount;
            if (currentAccount != null && (model.Amount > currentAccount.OperationalLimit
                || totalTransactionsAmount > currentAccount.OperationalLimit))
            {
                return (false, "Transaction Operation limit exceeded.");
            }
        }
        if (originMovement.Account.Customer.BankId != model.DestinyBankId)
        {
            return (false, "The destination bank does not match the entered bank.");
        }

        return (true, "All validations passed successfully");
    }
}
