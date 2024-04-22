using Core.Models;
using Core.Requests.Deposit;
using Core.Requests.Movements;

namespace Core.Interfaces.Repositories;

public interface IDepositRepository
{
    Task<DepositDTO> Deposit(CreateDepositModel model);
    Task<(bool isValid, string message)> DataValidation(CreateDepositModel model);


}
