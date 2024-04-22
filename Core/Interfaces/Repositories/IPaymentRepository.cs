using Core.Models;
using Core.Requests.Movements;
using Core.Requests.Payment;

namespace Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<PaymentDTO> Payment(CreatePaymentModel model);
}
