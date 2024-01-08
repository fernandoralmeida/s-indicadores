using IDN.Services.Empresa.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    => Ok(await _empresas
                    .DoReportEmpresasAsync(
                        _empresas.DoStoredProcedure(
                                            m!.ToUpper()), null));

}