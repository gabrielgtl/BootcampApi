using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IBankService
{
    Task<BankDTO> Add(CreateBankModel model);
    Task<BankDTO> GetById(int id);
    Task<BankDTO> Update(UpdateBankModel model);
    Task<bool> Delete(int id);
    Task<List<BankDTO>> GetAll();
}
