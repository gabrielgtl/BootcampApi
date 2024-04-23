using Core.Interfaces.Services;
using Core.Requests.Extraction;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ExtractionController : BaseApiController
{
    private readonly IExtractionService _service;

    public ExtractionController(IExtractionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Extraction([FromBody] CreateExtractionModel request)
    {
        return Ok(await _service.Extraction(request));
    }
}
