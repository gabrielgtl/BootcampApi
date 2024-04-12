using Core.Interfaces.Services;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CustomerController : BaseApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("filtered")]
    [Authorize(Roles = "Empleado, Admin, Invitado")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCustomersModel filter)
    {
        var customers = await _customerService.GetFiltered(filter);
        return Ok(customers);
    }

    [HttpPost("addCustomer")]
    [Authorize(Roles = "Empleado, Admin")]
    public  async Task<IActionResult> Create([FromBody] CreateCustomerModel request)
    {
        return Ok(await _customerService.Add(request));
    }
    [HttpPut("update")]
    [Authorize(Roles = "Empleado, Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerModel request)
    {
        return Ok(await _customerService.Update(request));
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Empleado, Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _customerService.Delete(id));
    }
    [HttpGet("getById/{id}")]
    //[Authorize(Roles = "Empleado, Admin, Invitado")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var customer = await _customerService.GetById(id);
        return Ok(customer);
    }
    [HttpGet("all")]
    [Authorize(Roles = "Empleado, Admin, Invitado")]
    public async Task<IActionResult> GetAll()
    {
        var customer = await _customerService.GetAll();
        return Ok(customer);
    }

}