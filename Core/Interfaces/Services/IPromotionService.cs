using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Services;

public interface IPromotionService
{
    Task<PromotionDTO> Add(CreatePromotionModel model);
    //  Task<List<PromotionDTO>> GetFiltered(FilterAccountModel filter);
    Task<PromotionDTO> Update(UpdatePromotionModel model);
    Task<PromotionDTO> GetById(int id);
    Task<bool> Delete(int id);
}
