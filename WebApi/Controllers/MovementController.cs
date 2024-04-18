using Core.Interfaces.Services;
using Core.Requests.Movements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class MovementController : BaseApiController
{
    private readonly IMovementService _service;

    public MovementController(IMovementService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovementModel request)
    {
        return Ok(await _service.Transference(request));
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var movement = await _service.GetById(id);
        return Ok(movement);
    }
}
