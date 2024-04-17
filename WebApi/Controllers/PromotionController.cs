using Core.Interfaces.Services;
using Core.Requests.Promotion;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PromotionController : BaseApiController
{
    private readonly IPromotionService _service;

    public PromotionController(IPromotionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePromotionModel request)
    {
        return Ok(await _service.Add(request));
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var promotion = await _service.GetById(id);
        return Ok(promotion);
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterPromotionModel filter)
    {
        var promotions = await _service.GetFiltered(filter);
        return Ok(promotions);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePromotionModel request)
    {
        return Ok(await _service.Update(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
