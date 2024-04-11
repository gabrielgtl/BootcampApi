using Core.Interfaces.Services;
using Core.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CreditCardController : BaseApiController
{
    private readonly ICreditCardService _creditCardService;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }
    [HttpGet("filtered")]
    [Authorize(Roles = "Empleado, Admin, Invitado")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCreditCardModel filter)
    {
        var creditCard = await _creditCardService.GetFiltered(filter);
        return Ok(creditCard);
    }

    [HttpPost("addCreditCard")]
    [Authorize(Roles = "Empleado, Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCreditCardModel request)
    {
        return Ok(await _creditCardService.Add(request));
    }
    [HttpPut("update")]
    [Authorize(Roles = "Empleado, Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCreditCardModel request)
    {
        return Ok(await _creditCardService.Update(request));
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Empleado, Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _creditCardService.Delete(id));
    }
    [HttpGet("getById/{id}")]
    [Authorize(Roles = "Empleado, Admin, Invitado")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var creditCard = await _creditCardService.GetById(id);
        return Ok(creditCard);
    }
}
