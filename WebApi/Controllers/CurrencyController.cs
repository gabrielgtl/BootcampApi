using Core.Interfaces.Services;
using Core.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CurrencyController : BaseApiController
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    [HttpGet("filterByName")]
    public async Task<IActionResult> GetByName([FromQuery] FilterCurrencyModel filter)
    {
        var currency = await _currencyService.GetByName(filter);
        return Ok(currency);
    }

    [HttpPost("addCurrency")]
    public async Task<IActionResult> Add([FromBody] CreateCurrencyModel request)
    {
        return Ok(await _currencyService.Add(request));
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCurrencyModel request)
    {
        return Ok(await _currencyService.Update(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _currencyService.Delete(id));
    }
    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var currency = await _currencyService.GetById(id);
        return Ok(currency);
    }
}
