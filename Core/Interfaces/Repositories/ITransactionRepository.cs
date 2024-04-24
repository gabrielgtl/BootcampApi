using Core.Models;
using Core.Requests.Transaction;

namespace Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<TransactionsDTO>> FilterTransaction(int id, FilterTransactionModel filter);
}
