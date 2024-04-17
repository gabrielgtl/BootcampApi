using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Enterprise;
using Infrastructure.Contexts;
using Mapster;

namespace Infrastructure.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly BootcampContext _context;

    public EnterpriseRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {
        var businessToCreate = model.Adapt<Enterprise>();

        _context.Enterprises.Add(businessToCreate);

        await _context.SaveChangesAsync();

        var bussinesDTO = businessToCreate.Adapt<EnterpriseDTO>();

        return bussinesDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var business = await _context.Enterprises.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id {id} not found");

        _context.Enterprises.Remove(business);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<EnterpriseDTO> GetById(int id)
    {
        var business = await _context.Enterprises.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id: {id} doest not exist");

        var businessDTO = business.Adapt<EnterpriseDTO>();

        return businessDTO;
    }

    public async Task<EnterpriseDTO> Update(UpdateEnterpriseModel model)
    {
        var businessToUpdate = model.Adapt<Enterprise>();

        _context.Enterprises.Update(businessToUpdate);

        await _context.SaveChangesAsync();

        var bussinesDTO = businessToUpdate.Adapt<EnterpriseDTO>();

        return bussinesDTO;
    }
}
