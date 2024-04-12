using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    Task<AccountDTO> Add(CreateAccountModel model);
    Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter);
    Task<AccountDTO> Update(UpdateAccountModel model);
    Task<AccountDTO> GetById(int id);
    Task<bool> Delete(int id);
}
