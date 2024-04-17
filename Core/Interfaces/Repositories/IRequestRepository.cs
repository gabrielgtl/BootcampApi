using Core.Models;
using Core.Requests.Request;

namespace Core.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<RequestDTO> Add(CreateRequestModel model);
    Task<RequestDTO> Update(UpdateRequestModel model);
    Task<RequestDTO> GetById(int id);

}
