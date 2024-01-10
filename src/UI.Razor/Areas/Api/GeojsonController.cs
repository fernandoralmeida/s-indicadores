using IDN.Services.Geojson.Interfaces;
using IDN.Services.Empresa.Interfaces;

using Microsoft.AspNetCore.Mvc;
using IDN.Data.Helpers;
using IDN.Core.Geojson.Models;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class GeojsonController : ControllerBase
{
    private readonly IServiceGeojson _geocode;
    private readonly IServiceEmpresa _empresas;

    public GeojsonController(IServiceGeojson geocode,
                                IServiceEmpresa empresas)
    {
        _geocode = geocode;
        _empresas = empresas;
    }

    [HttpGet("geojson/{m?}")]
    public async Task<IActionResult> GetGeojson([FromRoute] string? m)
    {
        var param = m ?? null;
        return Ok(await _geocode.DoGeojson(param));
    }

}