using Core.Models;
using Core.Requests.Deposit;

namespace Core.Interfaces.Services;

public interface IDepositService
{
    Task<DepositDTO> Deposit(CreateDepositModel model);
}
