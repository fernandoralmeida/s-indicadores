using System.Diagnostics;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using IDN.Services.Geojson.View;
using IDN.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages.Maps;

public class IndexModel : PageModel
{
    private readonly IMongoDB<REmpresas> _mongoDB;
    private readonly IMongoDB<VFeatures> _geojson;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? GeoCode { get; set; }
    public REmpresas? LReports { get; set; }
    public IEnumerable<REmpresas>? ListReports { get; set; }
    public RCharts? Charts { get; set; }
    public string? Empresasativas { get; set; }
    public IEnumerable<KeyValuePair<string, int>> Setores { get; set; } = new List<KeyValuePair<string, int>>();
    public string? LastDataExtraction { get; set; }
    public (RCharts g, REmpresas r) ControlCharts { get; set; } = new();
    public bool Zones_ON { get; set; } = true;

    public IndexModel(IServiceEmpresa empresa)
    {
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _geojson = Factory<VFeatures>.NewDataMongoDB();
        _empresa = empresa;
    }
    public async Task<ActionResult> OnGetAsync(string? m, string? zn)
    {
        if (string.IsNullOrEmpty(m))
            return RedirectToPage("/Index");

        if (zn == null)
            await LoadData(m);
        else
            await LoadDataZN(m, zn);

        if (m.Contains(','))
            Zones_ON = false;

        return Page();
    }

    public async Task LoadDataZN(string? m, string? zn)
    {
        // Stopwatch temporizador = new();
        // temporizador.Start();
        var a = DateTime.Now.Year;

        var _m = m ?? null;
        var _zn = zn!.DecryptURL();

        var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Geocode, _m);
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
                                    s => s.Bairro! == _zn
                                    && s.Municipio == Cidade!.ToUpper(),
                                    $"empresas_{m}_{zn}"));

        var __filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, Cidade?.ToUpper());
        foreach (var r in await _mongoDB.DoListAsync(__filter))
            LReports.EmpresasPorLocal = r.EmpresasPorLocal;

        Charts = await _empresa!.DoReportToChartAsync(LReports);

        ControlCharts = new(Charts!, LReports!);

        Cidade += $" : {_zn}";
        // temporizador.Stop();
    }


    public async Task LoadData(string? param)
    {
        var _param = param ?? null;

        var _cities = param?.Split(',');

        if (_cities?.Length > 0)
        {
            var _listREmpresas = new List<REmpresas>();
            Cidade = string.Empty;
            GeoCode = string.Empty;

            foreach (var city in _cities)
            {
                var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Geocode, city);
                foreach (var _geo in await _geojson.DoListAsync(__city))
                {
                    var _c = _geo.Properties!.Name?.ToUpper();
                    GeoCode += $"{_geo.Properties!.Geocode},";
                    Cidade += $"{_geo.Properties!.Name},";
                    var __filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _c);
                    foreach (var r in await _mongoDB.DoListAsync(__filter))
                        _listREmpresas.Add(r);
                }
            };
            Cidade = Cidade[..^1];
            GeoCode = GeoCode[..^1];
            ListReports = new List<REmpresas>(){
                    new() {
                        _id = Guid.NewGuid(),
                        Municipio = Cidade,

                        Quantitativo = SumAndGroup(_listREmpresas,e => e.Quantitativo!),

                        Quantitativo_Ano = SumAndGroup(_listREmpresas,e => e.Quantitativo_Ano!),

                        NovasEmpresas = SumAndGroup(_listREmpresas,e => e.NovasEmpresas!),

                        NovasEmpresas_Ano = SumAndGroup(_listREmpresas,e => e.NovasEmpresas_Ano!),
                        NovasMei_Ano = SumAndGroup(_listREmpresas,e => e.NovasMei_Ano!),
                        NovasME_Ano = SumAndGroup(_listREmpresas,e => e.NovasME_Ano!),
                        NovasEPP_Ano = SumAndGroup(_listREmpresas,e => e.NovasEPP_Ano!),
                        NovasDemais_Ano = SumAndGroup(_listREmpresas,e => e.NovasDemais_Ano!),

                        MatrizFilial = SumAndGroup(_listREmpresas,e => e.MatrizFilial!),

                        MatrizFilial_Ano = SumAndGroup(_listREmpresas,e => e.MatrizFilial_Ano!),

                        Baixas_Ano = SumAndGroup(_listREmpresas,e => e.Baixas_Ano!),

                        NaturezaJuridica = SumAndGroup(_listREmpresas,e => e.NaturezaJuridica!),

                        NaturezaJuridica_Ano = SumAndGroup(_listREmpresas,e => e.NaturezaJuridica_Ano!),

                        Setores = SumAndGroup(_listREmpresas,e => e.Setores!),

                        Setores_Ano = SumAndGroup(_listREmpresas,e => e.Setores_Ano!),

                        Fiscal = SumAndGroup(_listREmpresas,e => e.Fiscal!),

                        Fiscal_Ano = SumAndGroup(_listREmpresas,e => e.Fiscal_Ano!),

                        PorteS = SumAndGroup(_listREmpresas,e => e.PorteS!),

                        Simples = SumAndGroup(_listREmpresas,e => e.Simples!),

                        Simples_Ano = SumAndGroup(_listREmpresas,e => e.Simples_Ano!),

                        Idade = SumAndGroup(_listREmpresas,e => e.Idade!),

                        Rotatividade = _listREmpresas.SelectMany(e=>e.Rotatividade!)
                                                    .GroupBy(kvp => kvp.Key)
                                                    .Select(g => new KeyValuePair<string, float>(g.Key, g.Sum(kvp => kvp.Value) / _cities.Count())),

                        TAtividades = SumAndGroup_SubList(_listREmpresas, e => e.TAtividades!),
                        TAtividadesDescritivas = SumAndGroup_SubList(_listREmpresas, e => e.TAtividadesDescritivas!),
                        Top3Atividades = SumAndGroup_SubList_Top3(_listREmpresas, e => e.Top3Atividades!),
                        Top3Atividades_Ano = SumAndGroup_SubList_Top3(_listREmpresas, e => e.Top3Atividades_Ano!),
                        Porte = SumAndGroup_SubList(_listREmpresas, e => e.Porte!),
                        Porte_Ano = SumAndGroup_SubList(_listREmpresas, e => e.Porte_Ano!),
                        TaxaCrescimentoSetorial = SumAndGroup_SubList(_listREmpresas, e => e.TaxaCrescimentoSetorial!),
                        EmpresasPorLocal = SumAndGroup(_listREmpresas, e=> e.EmpresasPorLocal!),
                        EmpresasNovasPorLocal = SumAndGroup(_listREmpresas, e=> e.EmpresasNovasPorLocal!)
                        }
                };
        }
        else
        {
            Cidade = _param;
            var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Geocode, _param);
            foreach (var _geo in await _geojson.DoListAsync(__city))
            {
                var _c = _geo.Properties!.Name?.ToUpper();
                var _filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _param);
                ListReports = await _mongoDB.DoListAsync(_filter);
            }
        }
        foreach (var report in ListReports!)
        {
            foreach (var q in report.Quantitativo!.Where(s => s.Key == "Ativa"))
            {
                Empresasativas = $"Empresas ativas: {q.Value}";

                Setores = from s in report.Setores
                          select (new KeyValuePair<string, int>(s.Key, s.Value * 100 / q.Value));
            }
        }
        foreach (var report in ListReports)
        {
            LReports = report;
            Charts = await _empresa!.DoReportToChartAsync(report);
        }

        ControlCharts = new(Charts!, LReports!);
    }

    private static IEnumerable<KeyValuePair<string, int>> SumAndGroup(IEnumerable<REmpresas> list,
                                                            Func<REmpresas, IEnumerable<KeyValuePair<string, int>>> selector)
    {
        return list.SelectMany(selector)
                   .GroupBy(kvp => kvp.Key)
                   .Select(g => new KeyValuePair<string, int>(g.Key, g.Sum(kvp => kvp.Value)));
    }

    private static IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> SumAndGroup_SubList(
        IEnumerable<REmpresas> list,
        Func<REmpresas, IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>> selector)
    {
        return from l in list
                            .SelectMany(selector)
                            .GroupBy(g => g.Key)

               select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(l.Key,
                       from sl in l.SelectMany(e => e.Value)
                                   .GroupBy(g => g.Key)
                       select (new KeyValuePair<string, int>(sl.Key, sl.Sum(sm => sm.Value)))));
    }

    private static IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> SumAndGroup_SubList_Top3(
IEnumerable<REmpresas> list,
Func<REmpresas, IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>> selector)
    {
        return from l in list
                            .SelectMany(selector)
                            .GroupBy(g => g.Key)

               select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(l.Key,
                       from sl in l.SelectMany(e => e.Value)
                                   .GroupBy(g => g.Key)
                                    .OrderByDescending(k => k.Sum(e => e.Value))
                                    .Take(3)
                       select (new KeyValuePair<string, int>(sl.Key, sl.Sum(sm => sm.Value)))));
    }

    public async Task<PartialViewResult> OnGetZonesAsync(string c)
    {
        var _listREmpresas = new List<REmpresas>();
        var city = c;
        var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Geocode, city);
        foreach (var _geo in await _geojson.DoListAsync(__city))
        {
            var _c = _geo.Properties!.Name?.ToUpper();
            GeoCode += $"{_geo.Properties!.Geocode},";
            Cidade += $"{_geo.Properties!.Name},";
            var __filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _c);
            foreach (var r in await _mongoDB.DoListAsync(__filter))
                _listREmpresas.Add(r);
        }

        foreach (var report in _listREmpresas)
            LReports = report;

        return Partial("_Zones", LReports);
    }
    public async Task<ActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Cidade))
            return Page();

        var _cities = Cidade?.Split(',');

        if (_cities?.Length > 1)
        {
            Cidade = string.Empty;
            GeoCode = string.Empty;

            foreach (var city in _cities!)
            {
                var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, city);
                foreach (var _geo in await _geojson.DoListAsync(__city))
                {
                    GeoCode += $"{_geo.Properties!.Geocode},";
                    Cidade += $"{_geo.Properties!.Name},";
                }
            };
            Cidade = Cidade[..^1];
            GeoCode = GeoCode[..^1];
        }
        else
        {
            var _param = Cidade?.ToLower();
            var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _param);
            foreach (var _geo in await _geojson.DoListAsync(__city))
                GeoCode = _geo.Properties!.Geocode;
        }

        return RedirectToPage("/Maps/Index", new { m = GeoCode });
    }

}
