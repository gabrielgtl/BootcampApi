using Core.Models;
using Core.Requests.Customer;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model);
    Task<CustomerDTO> Update(UpdateCustomerModel model);
    Task<CustomerDTO> GetById(int id);
    Task<List<CustomerDTO>> GetAll();
    Task<bool> Delete(int id);
}