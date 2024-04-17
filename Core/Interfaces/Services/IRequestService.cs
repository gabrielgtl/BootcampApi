using Core.Models;
using Core.Requests.Request;

namespace Core.Interfaces.Services;

public interface IRequestService
{
    Task<RequestDTO> Add(CreateRequestModel model);
    Task<RequestDTO> Update(UpdateRequestModel model);
    Task<RequestDTO> GetById(int id);
}
