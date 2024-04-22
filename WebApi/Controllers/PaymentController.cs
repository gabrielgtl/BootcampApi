using Core.Interfaces.Services;
using Core.Requests.Payment;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [HttpPost("addPayment")]
    public async Task<IActionResult> Create(CreatePaymentModel request)
    {
        return Ok(await _paymentService.Payment(request));
    }

}
