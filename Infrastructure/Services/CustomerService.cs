using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Customer;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDTO> Add(CreateCustomerModel model)
    {
        return await _repository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter)
    {
        return await _repository.GetFiltered(filter);
    }

    public async Task<CustomerDTO> Update(UpdateCustomerModel model)
    {
        return await _repository.Update(model);
    }
    public async Task<List<CustomerDTO>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<CustomerDTO> GetById(int id)
    {
        return await _repository.GetById(id);
    }
}