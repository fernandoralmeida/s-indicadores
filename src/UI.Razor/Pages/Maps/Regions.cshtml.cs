using IDN.Core.Helpers;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages.Maps;

public class RegionsModel : PageModel
{
    private readonly IMongoDB<REmpresas> _mongoDB;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public REmpresas? LReports { get; set; }
    public IEnumerable<REmpresas>? ListReports { get; set; }
    public RCharts? Charts { get; set; } = null;
    public string? Empresasativas { get; set; }
    public IEnumerable<string>? Municipios { get; set; }
    public IEnumerable<KeyValuePair<string, int>> Setores { get; set; } = new List<KeyValuePair<string, int>>();
    public string? Titulo { get; set; }
    public string? Span_Result { get; set; }
    public string? LastDataExtraction { get; set; }
    // public string? RawPage { get; set; }
    public (RCharts g, REmpresas r) ControlCharts { get; set; }

    public RegionsModel(IServiceEmpresa empresa)
    {
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _empresa = empresa;
    }
    public async Task<ActionResult> OnGetAsync(string? m)
    {
        if (string.IsNullOrEmpty(m))
            return RedirectToPage("/Index");

        Cidade = m;
        Titulo = m.ToUpper();
        switch (m[..3])
        {
            case "ra-":
                var _c = m.Remove(0, 3).NormalizeText()!;
                Municipios = Regions.MacroRegoesRASP[_c.NormalizeText().ToLower()];
                Span_Result = $"Regiao Administrativa {_c}";
                break;
            case "rg-":
                var _rg = m.Remove(0, 3).NormalizeText()!;
                Municipios = Regions.MReigoesGovernoSP[_rg.NormalizeText().ToLower()];
                Span_Result = $"Regiao de Governo {_rg}";
                break;
            case "rm-":
                var _rm = m.Remove(0, 3).NormalizeText()!;
                Municipios = Regions.RMetropolitanasSP[_rm.NormalizeText().ToLower()];
                Span_Result = $"Regiao Metropolitana {_rm}";
                break;
            case "au-":
                var _au = m.Remove(0, 3).NormalizeText()!;
                Municipios = Regions.AUrbanosSP[_au.NormalizeText().ToLower()];
                Span_Result = $"Aglomerado Urbano {_au}";
                break;
            default:
                break;
        }

        await LoadData();
        return Page();
    }

    public async Task LoadData()
    {

        if (Municipios!.Count()! > 0)
        {
            var _listREmpresas = new List<REmpresas>();

            foreach (var city in Municipios!)
            {
                var _c = city.NormalizeText().ToUpper();
                var __filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _c);
                foreach (var r in await _mongoDB.DoListAsync(__filter))
                    _listREmpresas.Add(r);
            };

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
