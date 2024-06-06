using IDN.Core.Helpers;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using IDN.Services.Geojson.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages;

public class IndexModel : PageModel
{
    private readonly IMongoDB<REmpresas>? _mongoDB;
    private readonly IMongoDB<VFeatures> _geojson;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public string? GeoCode { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesAdministrativas { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesGovernoSP { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesMetropolitanasSP { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? AglomeradosUrbanosSP { get; set; }
    public REmpresas? LReports { get; set; }
    public IEnumerable<REmpresas>? ListReports { get; set; }
    public RCharts? Charts { get; set; } = null;
    public string? Empresasativas { get; set; }
    public IEnumerable<KeyValuePair<string, int>> Setores { get; set; } = new List<KeyValuePair<string, int>>();
    public (RCharts g, REmpresas r) ModelCharts { get; set; }
    public IEnumerable<string>? Municipios { get; set; }

    public IndexModel(IServiceEmpresa empresa)
    {
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _geojson = Factory<VFeatures>.NewDataMongoDB();
        _empresa = empresa;
    }

    public async Task OnGet()
    {
        var _list = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.MacroRegioesSP())
            _list.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_rg = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.RegioesGovernoSP())
            _list_rg.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_rm = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.RegioesMetropolitanasSP())
            _list_rm.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_au = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.AglomeradosUrbanosSP())
            _list_au.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        RegioesAdministrativas = _list;
        RegioesGovernoSP = _list_rg;
        RegioesMetropolitanasSP = _list_rm;
        AglomeradosUrbanosSP = _list_au;

        await LoadData();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
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

    public async Task LoadData()
    {

        var _listREmpresas = await _mongoDB!.DoListAsync();

        Municipios = from _m in _listREmpresas select (new string(_m.Municipio));

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
                                                    .Select(g => new KeyValuePair<string, float>(g.Key, g.Sum(kvp => kvp.Value) / Municipios.Count())),

                        TAtividades = SumAndGroup_SubList(_listREmpresas, e => e.TAtividades!),
                        TAtividadesDescritivas = SumAndGroup_SubList(_listREmpresas, e => e.TAtividadesDescritivas!),
                        Top3Atividades = SumAndGroup_SubList_Top3(_listREmpresas, e => e.Top3Atividades!),
                        Top3Atividades_Ano = SumAndGroup_SubList_Top3(_listREmpresas, e => e.Top3Atividades_Ano!),
                        Porte = SumAndGroup_SubList(_listREmpresas, e => e.Porte!),
                        Porte_Ano = SumAndGroup_SubList(_listREmpresas, e => e.Porte_Ano!),
                        TaxaCrescimentoSetorial = SumAndGroup_SubList(_listREmpresas, e => e.TaxaCrescimentoSetorial!)
                        }
                };


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

        ModelCharts = new(Charts!, LReports!);
    }

    private IEnumerable<KeyValuePair<string, int>> SumAndGroupTop3(IEnumerable<REmpresas> list,
                                                            Func<REmpresas, IEnumerable<KeyValuePair<string, int>>> selector)
    {
        return list.SelectMany(selector)
               .GroupBy(k => k.Key)
               .OrderByDescending(k => k.Sum(e => e.Value))
               .Take(3)
               .Select(g => new KeyValuePair<string, int>(g.Key, g.Sum(kvp => kvp.Value)));
    }

    private IEnumerable<KeyValuePair<string, int>> SumAndGroup(IEnumerable<REmpresas> list,
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
}
