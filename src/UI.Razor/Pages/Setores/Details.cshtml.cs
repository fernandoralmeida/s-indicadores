using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using IDN.Data.Interface;
using IDN.Core.Helpers;
using IDN.Services.Geojson.View;
using IDN.Data.Helpers;
using IDN.Services.Cnae.Interfaces;

namespace UI.Razor.Pages.Setores;

public partial class DetailsModel : PageModel
{
    private readonly ILogger<IndexModel>? _logger;
    private readonly IServiceEmpresa? _empresa;
    private readonly IMongoDB<VFeatures>? _geojson;
    private readonly IServiceCnae? _cnae;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? GeoCode { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? CNAE { get; set; }
    public REmpresas? LReports { get; set; }
    public RCharts? Charts { get; set; }
    public string? LastDataExtraction { get; set; }
    public string? Cnae { get; set; } = string.Empty;
    public string? MaxCnae { get; set; }
    public (RCharts g, REmpresas r) ControlCharts { get; set; }

    public DetailsModel(
        ILogger<IndexModel> logger,
        IServiceEmpresa empresa,
        IServiceCnae cnae)
    {
        _logger = logger;
        _empresa = empresa;
        _cnae = cnae;
        _geojson = Factory<VFeatures>.NewDataMongoDB();
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
        CNAE = m;

        var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Geocode, r);
        foreach (var _geo in await _geojson?.DoListAsync(__city)!)
        {
            var _c = _geo.Properties!.Name?.ToUpper();
            GeoCode = _geo.Properties!.Geocode;
            Cidade = _geo.Properties!.Name;
        }

        LReports = await _empresa!
                            .DoReportEmpresasAsync(
                                _empresa!
                                .DoListAsync(
                                    s => s.CnaeFiscalPrincipal!
                                    .StartsWith(m!)
                                    && s.Municipio == Cidade!.ToUpper(),
                                    $"empresas_{m}_{r}"));

        Charts = await _empresa!.DoReportToChartAsync(LReports);

        var _desc = await _cnae?.DoListAsync(s => s.Codigo!.StartsWith(m!))!;
        Cnae = m!.Length == 2 ? Dictionaries.CnaesSubClasses[m].NormalizeText() : _desc.FirstOrDefault()?.Descricao!.NormalizeText().ToUpper();

        foreach (var _c in LReports.Quantitativo!.Where(c => c.Key == "Ativa"))
            MaxCnae = _c.Value.ToString();

        ControlCharts = new(Charts!, LReports!);

        temporizador.Stop();

        _logger!.Log(LogLevel.Information, $"Consulta DoStoredProcedure({m} {r})");
    }
}

