using Core.Models;
using Core.Requests.Movements;

namespace Core.Interfaces.Repositories;

public interface IMovementRepository
{
    Task<MovementDTO> Transference(CreateMovementModel model);
    Task<MovementDTO> GetById(int id);
    Task<(bool isValid, string message)> DataValidation(CreateMovementModel model);
}
