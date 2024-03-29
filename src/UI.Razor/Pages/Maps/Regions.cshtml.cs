﻿using IDN.Core.Helpers;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages.Maps;

public class RegionsModel : PageModel
{
    private readonly IMongoDB<REmpresas> _mongoDB;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public IEnumerable<REmpresas>? LReports { get; set; }
    public string? Empresasativas { get; set; }
    public IEnumerable<string>? Municipios { get; set; }
    public IEnumerable<KeyValuePair<string, int>> Setores { get; set; } = new List<KeyValuePair<string, int>>();
    public string? Titulo { get; set; }

    public RegionsModel()
    {
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
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

            LReports = new List<REmpresas>(){
                    new() {
                        _id = Guid.NewGuid(),
                        Municipio = Cidade,

                        Quantitativo = _listREmpresas.SelectMany(e => e.Quantitativo!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Quantitativo_Ano = _listREmpresas.SelectMany(e => e.Quantitativo_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        NovasEmpresas = _listREmpresas.SelectMany(e => e.NovasEmpresas!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        NovasEmpresas_Ano = _listREmpresas.SelectMany(e => e.NovasEmpresas_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        MatrizFilial = _listREmpresas.SelectMany(e => e.MatrizFilial!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        MatrizFilial_Ano = _listREmpresas.SelectMany(e => e.MatrizFilial_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Baixas_Ano = _listREmpresas.SelectMany(e => e.Baixas_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        NaturezaJuridica = _listREmpresas.SelectMany(e => e.NaturezaJuridica!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        NaturezaJuridica_Ano = _listREmpresas.SelectMany(e => e.NaturezaJuridica_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Setores = _listREmpresas.SelectMany(e => e.Setores!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Setores_Ano = _listREmpresas.SelectMany(e => e.Setores_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Fiscal = _listREmpresas.SelectMany(e => e.Fiscal!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Fiscal_Ano = _listREmpresas.SelectMany(e => e.Fiscal_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        PorteS = _listREmpresas.SelectMany(e => e.PorteS!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Simples = _listREmpresas.SelectMany(e => e.Simples!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Simples_Ano = _listREmpresas.SelectMany(e => e.Simples_Ano!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        Idade = _listREmpresas.SelectMany(e => e.Idade!)
                                                        .GroupBy(kvp => kvp.Key)
                                                        .Select(g => new KeyValuePair<string, int>
                                                        (g.Key, g.Sum(kvp => kvp.Value))),

                        // TAtividades = _listREmpresas
                        //                     .SelectMany(m => m.TAtividades!)
                        //                     .GroupBy(g => g.Key)
                        //                     .Select(s => new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                        //                         (
                        //                             s.Key, s.Select(sc => new KeyValuePair<string, int>(sc.Key, 10))
                        //                         )
                        //                     )
                        TAtividades = from l in _listREmpresas
                                                        .SelectMany(e => e.TAtividades!)
                                                        .GroupBy(g => g.Key)

                                       select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(l.Key,
                                                from sl in l.SelectMany(e => e.Value)
                                                            .GroupBy(g=>g.Key)
                                                select (new KeyValuePair<string, int>(sl.Key, sl.Sum(sm=>sm.Value)))))

                        }
                };
        }

        foreach (var report in LReports!)
        {
            foreach (var q in report.Quantitativo!.Where(s => s.Key == "Ativa"))
            {
                Empresasativas = $"Empresas ativas: {q.Value}";

                Setores = from s in report.Setores
                          select (new KeyValuePair<string, int>(s.Key, s.Value * 100 / q.Value));
            }
        }
    }

    private IEnumerable<KeyValuePair<string, int>> SumAndGroup(IEnumerable<REmpresas> list,
                                                                Func<REmpresas, IEnumerable<KeyValuePair<string, int>>> selector)
    {
        return list.SelectMany(selector)
                   .GroupBy(kvp => kvp.Key)
                   .Select(g => new KeyValuePair<string, int>(g.Key, g.Sum(kvp => kvp.Value)));
    }

}