using Core.Models;
using Core.Requests.Currency;
namespace Core.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<CurrencyDTO> Add(CreateCurrencyModel model);
    Task<List<CurrencyDTO>> GetByName(FilterCurrencyModel filter);
    Task<CurrencyDTO> Update(UpdateCurrencyModel model);
    Task<CurrencyDTO> GetById(int id);
    Task<bool> Delete(int id);
}
