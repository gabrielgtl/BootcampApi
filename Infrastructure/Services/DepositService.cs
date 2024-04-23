using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Deposit;
namespace Infrastructure.Services;

public class DepositService : IDepositService
{
    private readonly IDepositRepository _depositRepository;

    public DepositService(IDepositRepository depositRepository)
    {
        _depositRepository = depositRepository;
    }

    public async Task<DepositDTO> Deposit(CreateDepositModel model)
    {
        var validationResult = await _depositRepository.DataValidation(model);
        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _depositRepository.Deposit(model);
    }
}
