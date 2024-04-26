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

        var originMovement = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .Where(m => m.Id == model.OriginAccountId)
            .FirstOrDefaultAsync();

        var destinationMovement = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .Where(m => m.Id == model.DestinationAccountId)
            .FirstOrDefaultAsync();


        if (originMovement == null || destinationMovement == null)
        {
            return (false, "Account does not exist");
        }
        

        if (originMovement.Type != destinationMovement.Type)
        {
            return (false, "Account with different account type");
        }

        if (model.Amount > originMovement.Balance)
        {
            return (false, "Amount is bigger than current balance");
        }

        if (originMovement.Status != AccountStatus.Active 
            && destinationMovement.Status != AccountStatus.Active)
        {
            return (false, "Account status is not active");
        }

        if (originMovement.Customer.BankId != destinationMovement.Customer.BankId)
        {
            if (string.IsNullOrEmpty(model.DocumentNumber) || string.IsNullOrEmpty(model.AccountNumber) || model.DestinyBankId == 0)
            {
                return (false, "Document number, account number, and destiny bank ID are required when transferring within the same bank.");
            }
            if (destinationMovement.Number != model.AccountNumber)
            {
                return (false, "Account number not valid");
            }

            if (destinationMovement.Customer.DocumentNumber != model.DocumentNumber)
            {
                return (false, "Customer document number not valid");
            }
            if (destinationMovement.CurrencyId != model.CurrencyId)
            {
                return (false, "Currency not valid");
            }
            if (originMovement.CurrencyId != destinationMovement.CurrencyId)
            {
                return (false, "Account with different currency");
            }
        }
        var transactionDate = model.TransferredDateTime;

        decimal totalExtractionsAmount = await _context.Extractions
            .Where(e => e.OperationDate.Month == transactionDate.Month &&
                        e.OperationDate.Year == transactionDate.Year &&
                        e.AccountId == model.OriginAccountId)
            .SumAsync(e => e.Amount);

        decimal totalDepositsAmount = await _context.Deposits
            .Where(d => d.OperationDate.Month == transactionDate.Month &&
                        d.OperationDate.Year == transactionDate.Year &&
                        d.AccountId == model.OriginAccountId)
            .SumAsync(d => d.Amount);

        decimal totalMovementsAmount = await _context.Movements
           .Where(m => (m.TransferredDateTime!.Value.Month == transactionDate.Month &&
                        m.TransferredDateTime!.Value.Year == transactionDate.Year &&
                        m.OriginAccountId == model.OriginAccountId) ||
                       (m.TransferredDateTime!.Value.Month == transactionDate.Month &&
                        m.TransferredDateTime!.Value.Year == transactionDate.Year &&
                        m.DestinationAccountId == model.OriginAccountId))
           .SumAsync(m => m.Amount);


        decimal totalTransactionsAmount =
            totalExtractionsAmount + totalDepositsAmount + totalMovementsAmount + model.Amount;
        if (originMovement.Type == AccountType.Current)
        {
            var currentAccount = originMovement.CurrentAccount;
            if (currentAccount != null && (model.Amount > currentAccount.OperationalLimit
                || totalTransactionsAmount > currentAccount.OperationalLimit))
            {
                return (false, "Transaction Operation limit exceeded.");
            }
        }
        var transactionDateDestiny = model.TransferredDateTime;

        decimal totalExtractionsAmountDestiny = await _context.Extractions
            .Where(e => e.OperationDate.Month == transactionDate.Month &&
                        e.OperationDate.Year == transactionDate.Year &&
                        e.AccountId == model.DestinationAccountId)
            .SumAsync(e => e.Amount);

        decimal totalDepositsAmountDestiny = await _context.Deposits
            .Where(d => d.OperationDate.Month == transactionDate.Month &&
                        d.OperationDate.Year == transactionDate.Year &&
                        d.AccountId == model.DestinationAccountId)
            .SumAsync(d => d.Amount);

        decimal totalMovementsAmountDestiny = await _context.Movements
           .Where(m => (m.TransferredDateTime!.Value.Month == transactionDate.Month &&
                        m.TransferredDateTime!.Value.Year == transactionDate.Year &&
                        m.OriginAccountId == model.DestinationAccountId) ||
                       (m.TransferredDateTime!.Value.Month == transactionDate.Month &&
                        m.TransferredDateTime!.Value.Year == transactionDate.Year &&
                        m.DestinationAccountId == model.DestinationAccountId))
           .SumAsync(m => m.Amount);


        decimal totalTransactionsAmountDestiny =
            totalExtractionsAmount + totalDepositsAmount + totalMovementsAmount + model.Amount;


        if (destinationMovement.Type == AccountType.Current)
        {
            var currentAccount = destinationMovement.CurrentAccount;
            if (currentAccount != null && (model.Amount > currentAccount.OperationalLimit
                || totalTransactionsAmount > currentAccount.OperationalLimit))
            {
                return (false, "Transaction Operation limit exceeded of Destination Account.");
            }
        }

        return (true, "All validations passed successfully");
    }
}
