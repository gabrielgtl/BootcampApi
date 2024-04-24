using Core.Models;
using Core.Requests.Transaction;

namespace Core.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionsDTO>> FilterTransaction(int id, FilterTransactionModel filter);
}
