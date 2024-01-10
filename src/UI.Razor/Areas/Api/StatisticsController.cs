using IDN.Data.Helpers;
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

    public StatisticsController(IServiceEmpresa empresas)
    {
        _empresas = empresas;
    }

    [HttpGet("chart-empresas/{m?}")]
    public async Task<IActionResult> DoCharts([FromRoute] string? m)
        => Ok(await _empresas
                        .DoReportToChartAsync(
                            await _empresas
                                    .DoReportEmpresasAsync(
                                        _empresas.DoStoredProcedure(
                                            m!.ToUpper()), null)));


    [HttpGet("report-empresas/{m?}")]
    public async Task<IActionResult> DoReport([FromRoute] string? m)
    {
        var _filter = m == null ? null : Builders<REmpresas>.Filter.Eq(e => e.Municipio, m);
        return Ok(await Factory<REmpresas>.NewDataMongoDB().DoListAsync(_filter));
    }
    

}