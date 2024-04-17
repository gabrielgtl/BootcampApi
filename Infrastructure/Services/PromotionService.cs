using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Promotion;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;

    public PromotionService(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<PromotionDTO> Add(CreatePromotionModel model)
    {
        return await _promotionRepository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _promotionRepository.Delete(id);
    }

    public async Task<PromotionDTO> GetById(int id)
    {
        return await _promotionRepository.GetById(id);

    }

    public async Task<List<PromotionDTO>> GetFiltered(FilterPromotionModel filter)
    {
        return await _promotionRepository.GetFiltered(filter);
    }

    public async Task<PromotionDTO> Update(UpdatePromotionModel model)
    {
        return await _promotionRepository.Update(model);

    }
}
