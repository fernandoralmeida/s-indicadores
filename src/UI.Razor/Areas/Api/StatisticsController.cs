using IDN.Core.Empresa.Models;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using IDN.Services.Geojson.View;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IServiceEmpresa _empresas;
    private readonly IMongoDB<REmpresas> _mongodb;

    public StatisticsController(IServiceEmpresa empresas)
    {
        _empresas = empresas;
        _mongodb = Factory<REmpresas>.NewDataMongoDB();
    }

    [HttpGet("charts/{m?}")]
    public async Task<IActionResult> DoCharts([FromRoute] string? m)
    {
        m = m?.ToUpper();

        var _filter = m == null ? null : Builders<REmpresas>.Filter.Eq(e => e.Municipio, m);

        var _charts = new List<RCharts>();

        foreach (var item in await _mongodb.DoListAsync(_filter))
        {
            _charts.Add(await _empresas.DoReportToChartAsync(item));
        }

        return Ok(_charts);
    }

    [HttpGet("reports/{m?}")]
    public async Task<IActionResult> DoReport([FromRoute] string? m)
    {
        m = m?.ToUpper();
        var _filter = m == null ? null : Builders<REmpresas>.Filter.Eq(e => e.Municipio, m);
        return Ok(await _mongodb.DoListAsync(_filter));
    }


    [HttpGet("cnae/{m}/{r?}")]
    public async Task<IActionResult> ListByCNAERegion([FromRoute] string m, [FromRoute] string? r)
    {
        try
        {
            var _list = new List<MEmpresa>();
            await foreach (var item in _empresas.DoStoredProcedure("cnaefiscalprincipal", m, r))
                _list.Add(item);

            var _list_report = new List<REmpresas>();
            var _list_m = _list.GroupBy(s => s.Municipio).OrderByDescending(s => s.Count());


            foreach (var item in _list_m.Where(s => s.Any(s => s.SituacaoCadastral == "Ativa")))
            {
                _list_report.Add(await _empresas.DoReportEmpresasAsync(item));
            }

            var _cities = from c in _list_m select new KeyValuePair<string, int>(c.Key, c.Count());

            var _mongoDB = Factory<VFeatures>.NewDataMongoDB();
            var _features = new List<VFeatures>();
            foreach (var city in _cities!)
            {
                var _m = city.Key.ToLower();
                var _filter = _m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _m);
                foreach (var item in await _mongoDB.DoListAsync(_filter))
                {
                    item.Properties!.Empresas = city.Value;
                    item.Properties!.Setor = _m;
                    _features.Add(item);
                }
            }

            return Ok(new VGeojson
            {
                Type = "FeatureCollection",
                Features = _features
            });
        }
        catch
        {
            return Ok(null);
        }

    }
    
}