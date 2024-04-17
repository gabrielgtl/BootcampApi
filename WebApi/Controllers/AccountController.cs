using Core.Interfaces.Services;
using Core.Requests.Account;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpPost("addAccount")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateAccountModel request)
    {
        return Ok(await _accountService.Add(request));
    }

    [HttpPut("updateAccount")]
    [AllowAnonymous]
    public async Task<IActionResult> Update([FromBody] UpdateAccountModel request)
    {
        return Ok(await _accountService.Update(request));
    }
    [HttpGet("filtered")]
    [AllowAnonymous]
    //[Authorize(Roles = "Empleado, Admin, Invitado")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterAccountModel filter)
    {
        var account = await _accountService.GetFiltered(filter);
        return Ok(account);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _accountService.GetById(id));

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _accountService.Delete(id));
    }
}
