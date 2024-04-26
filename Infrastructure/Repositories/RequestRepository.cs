using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Payment;
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

        if (request is null) throw new NotFoundException($"The request with id: {request!.Id} doest not exist");

        var currency = await _context.Currencies.FindAsync(model.CurrencyId);
        if (currency == null)
            throw new NotFoundException($"The currency with id: {model.CurrencyId} does not exist");

        var product = await _context.Products.FindAsync(model.ProductId);
        if (product == null)
            throw new NotFoundException($"The product with id: {model.ProductId} does not exist");

        var customer = await _context.Customers.FindAsync(model.CustomerId);
        if (customer == null)
            throw new NotFoundException($"The customer with id: {model.CustomerId} does not exist");

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
        
        var currency = await _context.Currencies.FindAsync(model.CurrencyId);
        if (currency == null)
            throw new NotFoundException($"The currency with id: {model.CurrencyId} does not exist");

        var product = await _context.Products.FindAsync(model.ProductId);
        if (product == null)
            throw new NotFoundException($"The product with id: {model.ProductId} does not exist");

        var customer = await _context.Customers.FindAsync(model.CustomerId);
        if (customer == null)
            throw new NotFoundException($"The customer with id: {model.CustomerId} does not exist");

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
