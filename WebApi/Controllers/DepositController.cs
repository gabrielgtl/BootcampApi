using Core.Interfaces.Services;
using Core.Requests.Deposit;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class DepositController : BaseApiController
{
    private readonly IDepositService _service;

    public DepositController(IDepositService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Deposit([FromBody] CreateDepositModel request)
    {
        return Ok(await _service.Deposit(request));
    }
}
