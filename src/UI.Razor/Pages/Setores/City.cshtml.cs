using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using System.Diagnostics;
using UI.Razor.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using IDN.Data.Helpers;
using MongoDB.Driver;
using IDN.Data.Interface;
using IDN.Core.Helpers;

namespace UI.Razor.Pages.Setores;

public partial class CityModel : PageModel
{
    private readonly ILogger<IndexModel>? _logger;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Municipio { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? CNAE { get; set; }
    public REmpresas? LReports { get; set; }
    public NavbarModel? NavModel { get; set; }
    public RCharts? Charts { get; set; }
    public string? LastDataExtraction { get; set; }


    public CityModel(
        ILogger<IndexModel> logger,
        IServiceEmpresa empresa)
    {
        _logger = logger;
        _empresa = empresa;
    }

    public async Task OnGetAsync(string m, string? r)
    {
        await DataLoad(m, r?.NormalizeText()!.ToLower()!);
        LastDataExtraction = "22-02-2024";
    }

    private async Task DataLoad(string m, string? r)
    {
        Stopwatch temporizador = new();
        temporizador.Start();
        var a = DateTime.Now.Year;

        var _m = m ?? null;
        Municipio = r;
        CNAE = m;

        LReports = await _empresa!
                            .DoReportEmpresasAsync(
                                _empresa!
                                .DoListAsync(
                                    s => s.CnaeFiscalPrincipal!
                                    .StartsWith(m)
                                    && s.Municipio == Municipio!.ToUpper(),
                                    $"empresas_{m}_{r}"));

        Charts = await _empresa!.DoReportToChartAsync(LReports);

        temporizador.Stop();
        NavModel = new()
        {
            Municipio = Municipio,
            Ano = a,
            Time = $"{temporizador.Elapsed:hh\\:mm\\:ss\\.fff}"
        };

        _logger!.Log(LogLevel.Information, $"Consulta DoStoredProcedure({m} {r})");
    }
}

