using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Movements;

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
        var validationResult = await _movementRepository.DataValidation(model);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _movementRepository.Transference(model);
    }

    public async Task<MovementDTO> GetById(int id)
    {
       return await _movementRepository.GetById(id);
    }
}
