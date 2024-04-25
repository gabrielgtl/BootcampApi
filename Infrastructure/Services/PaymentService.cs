using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests.Payment;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentDTO> Payment(CreatePaymentModel model)
    {
        var validationResult = await _paymentRepository.DataValidation(model);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _paymentRepository.Payment(model);
    }
}
