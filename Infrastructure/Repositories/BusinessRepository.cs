using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;

namespace Infrastructure.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly BootcampContext _context;

    public BusinessRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<BusinessDTO> Add(CreateBusinessModel model)
    {
        var businessToCreate = model.Adapt<Business>();

        _context.Bussineses.Add(businessToCreate);

        await _context.SaveChangesAsync();

        var bussinesDTO = businessToCreate.Adapt<BusinessDTO>();

        return bussinesDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var business = await _context.Bussineses.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id {id} not found");

        _context.Bussineses.Remove(business);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<BusinessDTO> GetById(int id)
    {
        var business = await _context.Bussineses.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id: {id} doest not exist");

        var businessDTO = business.Adapt<BusinessDTO>();

        return businessDTO;
    }

    public async Task<BusinessDTO> Update(UpdateBusinessModel model)
    {
        var businessToUpdate = model.Adapt<Business>();

        _context.Bussineses.Update(businessToUpdate);

        await _context.SaveChangesAsync();

        var bussinesDTO = businessToUpdate.Adapt<BusinessDTO>();

        return bussinesDTO;
    }
}
