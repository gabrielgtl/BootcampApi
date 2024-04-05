using Core.Interfaces.Services;
using Core.Requests;
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
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCustomersModel filter)
    {
        var customers = await _customerService.GetFiltered(filter);
        return Ok(customers);
    }

    [HttpPost("AgregarCustomer")]
    public  async Task<IActionResult> Create([FromBody] CreateCustomerModel request)
    {
        return Ok(await _customerService.Add(request));
    }
    [HttpPut("Actualizar")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerModel request)
    {
        return Ok(await _customerService.Update(request));
    }

}