using IDN.Services.Geojson.Interfaces;
using IDN.Services.Empresa.Interfaces;

using Microsoft.AspNetCore.Mvc;

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
        if(!string.IsNullOrEmpty(m))
            return Ok(await _geocode.DoGeojsonAsync(m));

        else        
            return Ok(await _geocode.ReadFileGeojsonAsync());        
            
    }
        

    [HttpGet("geojson-sp")]
    public async Task<IActionResult> GetGeojsonSP()
    {
        return Ok(await _geocode.ReadFileGeojson());
    }


    [HttpGet("geojson-to-db/{m?}")]
    public async Task<IActionResult> GetGeojsonToDB([FromRoute] string? m)
        => Ok(await _geocode.DoGeojsonToDBAsync(await _geocode.DoGeojsonAsync(m)));



}