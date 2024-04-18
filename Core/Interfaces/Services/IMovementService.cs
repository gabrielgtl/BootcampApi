using Core.Models;
using Core.Requests.Movements;

namespace Core.Interfaces.Services;

public interface IMovementService
{
    Task<MovementDTO> Transference(CreateMovementModel model);
    Task<MovementDTO> GetById(int id);
}
