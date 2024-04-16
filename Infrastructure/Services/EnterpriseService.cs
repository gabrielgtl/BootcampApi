using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _bussinesRepository;

    public EnterpriseService(IEnterpriseRepository businessRepository)
    {
        _bussinesRepository = businessRepository;
    }

    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {
        return await _bussinesRepository.Add(model);

    }

    public async Task<bool> Delete(int id)
    {
        return await _bussinesRepository.Delete(id);
    }

    public async Task<EnterpriseDTO> GetById(int id)
    {
        return await _bussinesRepository.GetById(id);
    }

    public async Task<EnterpriseDTO> Update(UpdateEnterpriseModel model)
    {
        return await _bussinesRepository.Update(model);
    }
}
