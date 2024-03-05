using IDN.Core.Cnae.Models;
using IDN.Services.Cnae.Interfaces;
using IDN.Services.Cnae.View;
using Microsoft.AspNetCore.Mvc;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class CnaeController : ControllerBase
{
    private readonly IServiceCnae _cnaes;

    public CnaeController(IServiceCnae cnaes)
    {
        _cnaes = cnaes;
    }

    [HttpGet("cnae/list")]
    public async Task<IActionResult> DoList()
    {
        return Ok(await _cnaes.DoListAsync());

    }

    [HttpGet("cnae/list/{c}")]
    public async Task<IActionResult> DoListParam([FromRoute] string c)
    {
        var _list = await _cnaes.DoListAsync(s => s.Codigo == c);
        return Ok(_list);
    }

    [HttpGet("cnae/list/sw/{c}")]
    public async Task<IActionResult> DoListStartWith([FromRoute] string c)
    {
        var _start = c[..2];
        var _list = await _cnaes.DoListAsync(s => s.Codigo!.StartsWith(_start));
        return Ok(_list);
    }
}