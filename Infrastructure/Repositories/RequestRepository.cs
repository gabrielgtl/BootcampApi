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

        if (request.Currency == null)throw new NotFoundException($"The currency with id: {request.CurrencyId} does not exist");
        if (request.Product == null) throw new NotFoundException($"The product with id: {request.ProductId} does not exist");
        if (request.Customer == null) throw new NotFoundException($"The product with id: {request.CustomerId} does not exist");

        _context.Requests.Add(request);

        await _context.SaveChangesAsync();

        var createdRequest = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .Include(a => a.Customer)
            .ThenInclude(a => a.Bank)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        return createdRequest.Adapt<RequestDTO>();
    }

    public async Task<RequestDTO> GetById(int id)
    {
        var request = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .Include(a => a.Customer)
            .ThenInclude(a => a.Bank)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request is null) throw new NotFoundException($"The request with id: {id} doest not exist");
        return request.Adapt<RequestDTO>();
    }

    public async Task<RequestDTO> Update(UpdateRequestModel model)
    {
        var request = model.Adapt<Request>();
        if (request is null) throw new NotFoundException($"The request with id: {request!.Id} doest not exist");
        if (request.Currency == null) throw new NotFoundException($"The currency with id: {request.CurrencyId} does not exist");
        if (request.Product == null) throw new NotFoundException($"The product with id: {request.ProductId} does not exist");
        if (request.Customer == null) throw new NotFoundException($"The product with id: {request.CustomerId} does not exist");

        _context.Requests.Update(request);

        await _context.SaveChangesAsync();

        var createdRequest = await _context.Requests
            .Include(a => a.Currency)
            .Include(a => a.Product)
            .Include(a => a.Customer)
            .ThenInclude(a => a.Bank)
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        return createdRequest.Adapt<RequestDTO>();
    }
}
