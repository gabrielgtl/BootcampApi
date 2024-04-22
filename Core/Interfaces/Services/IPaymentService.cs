using Core.Models;
using Core.Requests.Payment;

namespace Core.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentDTO> Payment(CreatePaymentModel model);

}
