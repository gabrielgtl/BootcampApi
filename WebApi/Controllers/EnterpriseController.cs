using Core.Interfaces.Services;
using Core.Requests.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class EnterpriseController : BaseApiController
{
    private readonly IEnterpriseService _service;

    public EnterpriseController(IEnterpriseService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseModel request)
    {
        return Ok(await _service.Add(request));
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var business = await _service.GetById(id);
        return Ok(business);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEnterpriseModel request)
    {
        return Ok(await _service.Update(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
