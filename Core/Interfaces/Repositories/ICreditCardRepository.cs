using Core.Models;
using Core.Requests.CreditCard;
namespace Core.Interfaces.Repositories;

public interface ICreditCardRepository
{
    Task<CreditCardDTO> Add(CreateCreditCardModel model);
    Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter);
    Task<CreditCardDTO> Update(UpdateCreditCardModel model);
    Task<CreditCardDTO> GetById(int id);
    Task<bool> Delete(int id);
}
