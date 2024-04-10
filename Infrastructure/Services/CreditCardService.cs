

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests;

namespace Infrastructure.Services;

public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardRepository _creditCardRepository;

    public CreditCardService(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;
    }

    public async Task<CreditCardDTO> Add(CreateCreditCardModel model)
    {
        return await _creditCardRepository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _creditCardRepository.Delete(id);
    }

    public async Task<CreditCardDTO> GetById(int id)
    {
        return await _creditCardRepository.GetById(id);
    }

    public async Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter)
    {
        return await _creditCardRepository.GetFiltered(filter);
    }

    public async Task<CreditCardDTO> Update(UpdateCreditCardModel model)
    {
        return await _creditCardRepository.Update(model);
    }
}
