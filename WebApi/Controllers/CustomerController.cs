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

    [HttpPost("addCustomer")]
    public  async Task<IActionResult> Create([FromBody] CreateCustomerModel request)
    {
        return Ok(await _customerService.Add(request));
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerModel request)
    {
        return Ok(await _customerService.Update(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _customerService.Delete(id));
    }
    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var customer = await _customerService.GetById(id);
        return Ok(customer);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var customer = await _customerService.GetAll();
        return Ok(customer);
    }

}