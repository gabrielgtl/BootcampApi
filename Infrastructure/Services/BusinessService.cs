using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _bussinesRepository;

    public BusinessService(IBusinessRepository businessRepository)
    {
        _bussinesRepository = businessRepository;
    }

    public async Task<BusinessDTO> Add(CreateBusinessModel model)
    {
        return await _bussinesRepository.Add(model);

    }

    public async Task<bool> Delete(int id)
    {
        return await _bussinesRepository.Delete(id);
    }

    public async Task<BusinessDTO> GetById(int id)
    {
        return await _bussinesRepository.GetById(id);
    }

    public async Task<BusinessDTO> Update(UpdateBusinessModel model)
    {
        return await _bussinesRepository.Update(model);
    }
}
