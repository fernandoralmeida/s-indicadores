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
using MongoDB.Driver.Core.Authentication;
using MongoDB.Bson;
using System.Net.Mail;

namespace UI.Razor.Pages.Empresas;

public partial class IndexModel : PageModel
{
    private readonly IMongoDB<REmpresas>? _mongoDB;
    private readonly ILogger<IndexModel>? _logger;
    private readonly IServiceEmpresa? _empresa;

    [BindProperty(SupportsGet = true)]
    public string? Municipio { get; set; }
    public REmpresas? LReports { get; set; }
    public IEnumerable<REmpresas>? ListReports { get; set; }
    public NavbarModel? NavModel { get; set; }
    public RCharts? Charts { get; set; }

    public IndexModel(
        ILogger<IndexModel> logger,
        IServiceEmpresa empresa)
    {
        _logger = logger;
        _empresa = empresa;
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
    }

    public async Task OnGetAsync(string m)
    {
        await DataLoad(m.NormalizeText().ToLower());
    }

    public async Task OnPostAsync()
    {
        await DataLoad(Municipio!);
    }

    private async Task DataLoad(string param)
    {
        Stopwatch temporizador = new();
        temporizador.Start();
        var a = DateTime.Now.Year;

        var _param = param ?? null;

        var _cities = param?.Split(',');

        if (_cities?.Length > 0)
        {
            var _listREmpresas = new List<REmpresas>();
            Municipio = string.Empty;

            foreach (var city in _cities)
            {
                var _c = city.Trim().ToUpper();
                Municipio += $"{city.Trim()},";
                var __filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _c);
                foreach (var r in await _mongoDB!.DoListAsync(__filter))
                    _listREmpresas.Add(r);
            };
            Municipio = Municipio[..^1];
            ListReports = new List<REmpresas>(){
                    new() {
                        _id = Guid.NewGuid(),
                        Municipio = Municipio,

                        Quantitativo = SumAndGroup(_listREmpresas,e => e.Quantitativo!),

                        Quantitativo_Ano = SumAndGroup(_listREmpresas,e => e.Quantitativo_Ano!),

                        NovasEmpresas = SumAndGroup(_listREmpresas,e => e.NovasEmpresas!),

                        NovasEmpresas_Ano = SumAndGroup(_listREmpresas,e => e.NovasEmpresas_Ano!),

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
                        Top3Atividades = SumAndGroup_SubList(_listREmpresas, e => e.Top3Atividades!),
                        Top3Atividades_Ano = SumAndGroup_SubList(_listREmpresas, e => e.Top3Atividades_Ano!),
                        Porte = SumAndGroup_SubList(_listREmpresas, e => e.Porte!),
                        Porte_Ano = SumAndGroup_SubList(_listREmpresas, e => e.Porte_Ano!),
                        TaxaCrescimentoSetorial = SumAndGroup_SubList(_listREmpresas, e => e.TaxaCrescimentoSetorial!)
                        }
                };
        }
        else
        {
            Municipio = _param;
            var _c = _param?.ToUpper();
            var _filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, _param);
            ListReports = await _mongoDB!.DoListAsync(_filter);
        }
        foreach (var report in ListReports)
        {
            LReports = report;
            Charts = await _empresa!.DoReportToChartAsync(report);
        }


        temporizador.Stop();
        NavModel = new()
        {
            Municipio = Municipio,
            Ano = a,
            Time = $"{temporizador.Elapsed:hh\\:mm\\:ss\\.fff}"
        };

        _logger!.Log(LogLevel.Information, $"Consulta DoStoredProcedure({Municipio})");
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

    [GeneratedRegex("\\[|\\]", RegexOptions.IgnoreCase, "pt-BR")]
    private static partial Regex CRegex();
}

