using Core.Models;
using Core.Requests.Promotion;

namespace Core.Interfaces.Services;

public interface IPromotionService
{
    Task<PromotionDTO> Add(CreatePromotionModel model);
    Task<List<PromotionDTO>> GetFiltered(FilterPromotionModel filter);
    Task<PromotionDTO> Update(UpdatePromotionModel model);
    Task<PromotionDTO> GetById(int id);
    Task<bool> Delete(int id);
}
