using IDN.Services.Cnae.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Core.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Records;
using IDN.Services.Geojson.View;
using IDN.Services.Empresa.Interfaces;
using IDN.Data.Helpers;

namespace UI.Razor.Pages.Setores;

public class IndexModel : PageModel
{
    private readonly IServiceCnae _cnaes;
    private readonly IMongoDB<REmpresas>? _mongoDB;
    private readonly IMongoDB<VFeatures> _geojson;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }
    public string? UrlAPI { get; set; } = "/api/v1/geojson/";
    public string? Cidade { get; set; }
    public string? Empresasativas { get; set; }
    public string? Cnae { get; set; } = "Indicadores.net";
    public string? MapMode { get; set; } = "0";
    public string? PageParam { get; set; } = string.Empty;
    public REmpresas? LReports { get; set; }
    public RCharts? Charts { get; set; } = null;
    public IEnumerable<string>? Municipios { get; set; }
    public IEnumerable<REmpresas>? ListReports { get; set; }
    public IEnumerable<KeyValuePair<string, int>> Setores { get; set; } = new List<KeyValuePair<string, int>>();
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<string>>>>>? Setore_Segmentos_Cnaes { get; set; }
    public (RCharts g, REmpresas r) ModelCharts { get; set; }
    public IndexModel(IServiceCnae cnae,
                        IServiceEmpresa empresa)
    {
        _cnaes = cnae;
        _empresa = empresa;
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _geojson = Factory<VFeatures>.NewDataMongoDB();
    }

    public async Task OnGetAsync(string? n)
    {
        PageParam = n;
        await DoListCnae(n!);
        if (string.IsNullOrEmpty(n))
            await LoadData();
        else
        {
            await LoadDataCnae(n!);
            var _tipo = n.Length == 2 ? "segmento" : "cnae";
            UrlAPI = $"/api/v1/geojson/{_tipo}/{n}";
            MapMode = "1";
        }
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

    private async Task DoListCnae(string param)
    {
        var _list = new List<(string cnae, string desc, string setor)>();

        var _test_search = param?.ToLower().NormalizeText();

        var _list_cnae = await _cnaes.DoListAsync();

        foreach (var item in _list_cnae)
            _list.Add(new(item.Codigo!, item.Descricao!, Dictionaries.SetorProdutivo[item.Codigo![..2]]));

        Setore_Segmentos_Cnaes = from st in _list
                                                .GroupBy(g => g.setor)

                                 select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<string>>>>(st.Key,
                                    from sg in st
                                                .Where(s => s.cnae.StartsWith(s.cnae[..2]))
                                                .GroupBy(g => g.cnae[..2])
                                    select (new KeyValuePair<string, IEnumerable<string>>($"{sg.Key[..2]} {Dictionaries.CnaesSubClasses[sg.Key[..2]]}",
                                        from ce in _list
                                                        .Where(s => s.cnae!.StartsWith(sg.Key[..2]))
                                        select new string($"{ce.cnae} {ce.desc}")
                                        ))
                                 ));
    }

    private async Task LoadDataCnae(string param)
    {
        var _cnae = await _cnaes.DoListAsync(s => s.Codigo!.StartsWith(param));

        Cnae = param.Length == 2 ? Dictionaries.CnaesSubClasses[param].NormalizeText() : _cnae.FirstOrDefault()?.Descricao!.NormalizeText();

        LReports = await _empresa?.
                            DoReportEmpresasAsync(
                                _empresa.DoListAsync(
                                    s =>
                                    s.CnaeFiscalPrincipal!.StartsWith(param) &&
                                    s.SituacaoCadastral == "02",
                                    $"setor_{param}"))!;

        Setores = from s in LReports.Setores
                  select (new KeyValuePair<string, int>(s.Key, s.Value));

        Charts = await _empresa!.DoReportToChartAsync(LReports);
        ModelCharts = new(Charts!, LReports!);
    }
}
