using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests.Movements;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly BootcampContext _context;

    public MovementRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<MovementDTO> Transference(CreateMovementModel model)
    {
        var movement = model.Adapt<Movement>();

        _context.Movements.Add(movement);

        await _context.SaveChangesAsync();

        var createdMovement = await _context.Movements
            .Include(a => a.Account)
            .FirstOrDefaultAsync(a => a.Id == movement.Id);

        return createdMovement.Adapt<MovementDTO>();
    }

    public async Task<MovementDTO> GetById(int id)
    {
        var movement = await _context.Movements
            .Include(a => a.Account)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (movement is null) throw new NotFoundException($"The account with id: {id} doest not exist");

        return movement.Adapt<MovementDTO>();
    }
}
