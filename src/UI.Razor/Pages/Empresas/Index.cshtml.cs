using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using System.Diagnostics;
using UI.Razor.Pages.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using IDN.Data.Helpers;
using MongoDB.Driver;

namespace UI.Razor.Pages.Empresas;

public partial class IndexModel : PageModel
{
    private readonly ILogger<IndexModel>? _logger;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? MunicipioAtivo { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ZonaAtiva { get; set; }
    public IEnumerable<string>? Municipios { get; set; }
    public REmpresas? LReports { get; set; }
    public NavbarModel? NavModel { get; set; }
    public SelectList? Zonas { get; set; }
    public RCharts? Charts { get; set; }
    public string[]? Local { get; set; } = new string[2];

    public IndexModel(
        ILogger<IndexModel> logger,
        IServiceEmpresa empresa)
    {
        _logger = logger;
        _empresa = empresa;
    }

    public async Task OnGetAsync(string m)
    {
        await DataLoad(m);
    }

    public async Task OnPostAsync()
    {
        await DataLoad(MunicipioAtivo!);
    }

    private async Task DataLoad(string param)
    {
        MunicipioAtivo = param;
        var a = DateTime.Now.Year;
        Stopwatch temporizador = new();
        temporizador.Start();
        _logger!.Log(LogLevel.Information, $"Consulta DoStoredProcedure({MunicipioAtivo})");

        Local = string.IsNullOrEmpty(ZonaAtiva) ? new string[2] : CRegex().Replace(ZonaAtiva!, "").Split(',');

        var _mongoDB = Factory<REmpresas>.NewDataMongoDB();

        var filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, MunicipioAtivo.ToUpper());

        foreach (var item in await _mongoDB.DoListAsync(filter))
        {
            LReports = item;
            Charts = await _empresa!.DoReportToChartAsync(item);
            Zonas = new SelectList(item.EmpresasNovasPorLocal);
        }

        temporizador.Stop();
        NavModel = new()
        {
            Municipio = MunicipioAtivo,
            Ano = a,
            Time = $"{temporizador.Elapsed:hh\\:mm\\:ss\\.fff}"
        };
    }

    [GeneratedRegex("\\[|\\]", RegexOptions.IgnoreCase, "pt-BR")]
    private static partial Regex CRegex();
}

