using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Services;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model);
    Task<CustomerDTO> Update(UpdateCustomerModel model);
    Task<CustomerDTO> GetById(int id);
    Task<List<CustomerDTO>> GetAll();
    Task<bool> Delete(int id);
}