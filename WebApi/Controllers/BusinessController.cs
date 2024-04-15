﻿using Core.Interfaces.Services;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BusinessController : BaseApiController
{
    private readonly IBusinessService _service;

    public BusinessController(IBusinessService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessModel request)
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
    public async Task<IActionResult> Update([FromBody] UpdateBusinessModel request)
    {
        return Ok(await _service.Update(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
