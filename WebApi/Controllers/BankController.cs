using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BankController : BaseApiController
{
    private readonly IBankService _service;

    public BankController(IBankService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBankModel request)
    {
        return Ok(await _service.Add(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var bank = await _service.GetById(id);
        return Ok(bank);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBankModel request)
    {
        return Ok(await _service.Update(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var banks = await _service.GetAll();
        return Ok(banks);
    }
}
