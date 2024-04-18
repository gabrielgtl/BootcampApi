using Core.Interfaces.Services;
using Core.Requests.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class RequestController : BaseApiController
{
    private readonly IRequestService _service;

    public RequestController(IRequestService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestModel request)
    {
        return Ok(await _service.Add(request));
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var request = await _service.GetById(id);
        return Ok(request);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRequestModel request)
    {
        return Ok(await _service.Update(request));
    }
}
