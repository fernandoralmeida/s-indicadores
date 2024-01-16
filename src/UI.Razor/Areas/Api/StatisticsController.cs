using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
}