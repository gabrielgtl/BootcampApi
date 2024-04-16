using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IEnterpriseRepository
{
    Task<EnterpriseDTO> Add(CreateEnterpriseModel model);
    Task<EnterpriseDTO> GetById(int id);
    Task<EnterpriseDTO> Update(UpdateEnterpriseModel model);
    Task<bool> Delete(int id);
}
