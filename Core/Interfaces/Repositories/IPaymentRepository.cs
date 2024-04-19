using Core.Models;
using Core.Requests;
using Core.Requests.Movements;

namespace Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<MovementDTO> Payment(CreatePaymentModel model);
}
