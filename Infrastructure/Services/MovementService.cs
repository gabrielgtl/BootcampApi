using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Movements;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovementService : IMovementService
{
    private readonly IMovementRepository _movementRepository;

    public MovementService(IMovementRepository movementRepository)
    {
        _movementRepository = movementRepository;
    }

    public async Task<MovementDTO> Transference(CreateMovementModel model)
    {
        return await _movementRepository.Transference(model);
    }

    public async Task<MovementDTO> GetById(int id)
    {
       return await _movementRepository.GetById(id);
    }
}
