using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IPromotionRepository
{
    Task<PromotionDTO> Add(CreatePromotionModel model);
  //  Task<List<PromotionDTO>> GetFiltered(FilterAccountModel filter);
    Task<PromotionDTO> Update(UpdatePromotionModel model);
    Task<PromotionDTO> GetById(int id);
    Task<bool> Delete(int id);
}
