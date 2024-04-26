using Core.Models;
using Core.Requests.Payment;
using Core.Requests.Request;

namespace Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<PaymentDTO> Payment(CreatePaymentModel model);
    Task<(bool isValid, string message)> DataValidation(CreatePaymentModel model);

}
