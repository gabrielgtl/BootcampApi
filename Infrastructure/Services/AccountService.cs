using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<AccountDTO> Add(CreateAccountModel model)
    {
        return await _accountRepository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _accountRepository.Delete(id);
    }

    public Task<AccountDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        return await _accountRepository.GetFiltered(filter);
    }

    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        return await _accountRepository.Update(model);
    }
}
