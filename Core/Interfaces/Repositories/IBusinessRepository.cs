using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IBusinessRepository
{
    Task<BusinessDTO> Add(CreateBusinessModel model);
    Task<BusinessDTO> GetById(int id);
    Task<BusinessDTO> Update(UpdateBusinessModel model);
    Task<bool> Delete(int id);
}
