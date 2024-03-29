using IDN.Services.Geojson.Interfaces;
using IDN.Services.Empresa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IDN.Data.Helpers;
using IDN.Services.Geojson.View;
using IDN.Services.Cnae.Interfaces;
using IDN.Core.Empresa.Models;
using IDN.Services.Empresa.Records;
using MongoDB.Driver;
using IDN.Data.Interface;
using MongoDB.Bson;
using IDN.Core.Helpers;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class GeocodeController : ControllerBase
{
    private readonly IMongoDB<VFeatures>? _geocode;
    public GeocodeController(IServiceGeojson geocode,
                                IServiceEmpresa empresas,
                                IServiceCnae cnaes)
    {
        _geocode = Factory<VFeatures>.NewDataMongoDB();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    [HttpGet("geocode/{m?}")]
    public async Task<IActionResult> GetGeocode([FromRoute] string? m)
    {
        try
        {
            var _cities = Regions.MacroRegoesRASP[m!];

            var _return = new List<string>();
            var _features = new List<VFeatures>();

            foreach (var city in _cities!)
            {
                var _c = city.NormalizeText().ToLower();
                var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _c);
                foreach (var _geo in await _geocode!.DoListAsync(__city))
                {
                    _return.Add($"{city!},{_geo.Properties!.Geocode!}");
                }
            };

            return Ok(_return);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

}