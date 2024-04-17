using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Account;
using Core.Requests.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;

    public RequestService(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<RequestDTO> Add(CreateRequestModel model)
    {
        return await _requestRepository.Add(model);
    }

    public async Task<RequestDTO> GetById(int id)
    {
        return await _requestRepository.GetById(id);
    }

    public async Task<RequestDTO> Update(UpdateRequestModel model)
    {
        return await _requestRepository.Update(model);
    }
}
