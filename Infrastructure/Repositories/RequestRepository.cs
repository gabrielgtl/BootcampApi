using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Account;
using Core.Requests.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly BootcampContext _context;

    public RequestRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<RequestDTO> Add(CreateRequestModel model)
    {
        var request = model.Adapt<Request>();

        _context.Requests.Add(request);

        await _context.SaveChangesAsync();

        var createdRequest = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        return createdRequest.Adapt<RequestDTO>();
    }

    public async Task<RequestDTO> GetById(int id)
    {
        var request = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request is null) throw new NotFoundException($"The request with id: {id} doest not exist");

        return request.Adapt<RequestDTO>();
    }

    public async Task<RequestDTO> Update(UpdateRequestModel model)
    {
        var request = model.Adapt<Request>();

        _context.Requests.Update(request);

        await _context.SaveChangesAsync();

        var createdRequest = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        return createdRequest.Adapt<RequestDTO>();
    }
}
