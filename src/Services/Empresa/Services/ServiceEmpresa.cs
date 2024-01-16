using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Records;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Models;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;
using IDN.Core.Helpers;
using System.Text;

namespace IDN.Services.Empresa.Services;

public class ServiceEmpresa : IServiceEmpresa
{
    private readonly IServiceCoreEmpresa _empresa;
    private readonly IMemoryCache _memorycache;
    public ServiceEmpresa(IServiceCoreEmpresa empresa,
        IMemoryCache memorycache)
    {
        _empresa = empresa;
        _memorycache = memorycache;
    }

    public async Task<REmpresas> DoReportEmpresasAsync(IAsyncEnumerable<MEmpresa> lista, Func<MEmpresa, bool>? param = null)
    {
        var ano = DateTime.Now.Year;
        var _lista = new List<MEmpresa>();

        await foreach (var item in lista)
            _lista.Add(item);

        var emps_bairro = _lista.Where(s => s.SituacaoCadastral!.ToLower() == "ativa");
        var emps_novas_bairro = emps_bairro.Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()));

        var emps_raw = _lista;
        if (param != null)
            emps_raw = _lista.Where(param).ToList();

        var emps = emps_raw.Where(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year <= ano);

        var emps_ativas = emps.Where(s => s.SituacaoCadastral!.ToLower() == "ativa");
        var emps_baixadas = emps.Where(s => s.SituacaoCadastral!.ToLower() == "baixada");

        return await Task.Run(() => new REmpresas()
        {
            Municipio = emps_ativas.FirstOrDefault()!.Municipio!,

            Quantitativo = Quantitativo(emps),

            Quantitativo_Ano = from st_a in emps.Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                                .GroupBy(g => g.SituacaoCadastral)
                                                .OrderByDescending(o => o.Count())
                               select (new KeyValuePair<string, int>(st_a.Key, st_a.Count())),

            NovasEmpresas = from qtm in emps_ativas
                                        .OrderBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM"))
                                        .GroupBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMM"))
                            select (new KeyValuePair<string, int>(qtm.Key, qtm.Count())),

            NovasEmpresas_Ano = from qtm in emps_ativas
                                        .Where(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date >= DateTime.Now.AddMonths(-12))

                                        .OrderBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date)
                                        .GroupBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMM"))
                                select (new KeyValuePair<string, int>(qtm.Key, qtm.Count())),

            MatrizFilial = from mf in emps_ativas
                                        .GroupBy(s => s.IdentificadorMatrizFilial)
                                        .OrderByDescending(s => s.Count())
                           select (new KeyValuePair<string, int>(mf.Key, mf.Count())),

            MatrizFilial_Ano = from mfa in emps_ativas
                                        .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                        .GroupBy(s => s.IdentificadorMatrizFilial)
                                        .OrderByDescending(s => s.Count())
                               select (new KeyValuePair<string, int>(mfa.Key, mfa.Count())),


            Baixas_Ano = from qtm in emps_baixadas
                            .Where(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date >= DateTime.Now.AddMonths(-12))
                            .OrderBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date)
                            .GroupBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMM"))
                         select (new KeyValuePair<string, int>(qtm.Key, qtm.Count())),

            NaturezaJuridica = from nj in emps_ativas
                                            .GroupBy(g => g.NaturezaJuridica)
                                            .OrderByDescending(o => o.Count())
                               select (new KeyValuePair<string, int>(nj.Key, nj.Count())),

            NaturezaJuridica_Ano = from nja in emps_ativas
                                            .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                            .GroupBy(g => g.NaturezaJuridica)
                                            .OrderByDescending(o => o.Count())
                                   select (new KeyValuePair<string, int>(nja.Key, nja.Count())),

            Setores = from ata in emps_ativas
                            .GroupBy(s => s.SetorProdutivo())
                            .OrderByDescending(o => o.Count())
                      select (new KeyValuePair<string, int>(ata.Key, ata.Count())),

            Setores_Ano = from atm in emps_ativas
                                .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                .GroupBy(s => s.SetorProdutivo())
                                .OrderByDescending(o => o.Count())
                          select (new KeyValuePair<string, int>(atm.Key, atm.Count())),

            TAtividades = from t3 in emps_ativas
                                .GroupBy(s => s.SetorProdutivo())
                                .OrderByDescending(s => s.Count())
                          select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(t3.Key,
                                                                  from tp3 in t3
                                                                      .Where(s => s.SetorProdutivo() == t3.Key)
                                                                      .GroupBy(s => s.CnaeFiscalPrincipal![..2])
                                                                      .OrderByDescending(s => s.Count())
                                                                  select (new KeyValuePair<string, int>(Dictionaries.CnaesSubClasses[tp3.Key], tp3.Count())))),

            TAtividadesDescritivas = from t3 in emps_ativas
                                        .GroupBy(s => s.SetorProdutivo())
                                        .OrderByDescending(s => s.Count())
                                     select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(t3.Key,
                                                                   from tp3 in t3
                                                                       .Where(s => s.SetorProdutivo() == t3.Key)
                                                                       .GroupBy(s => s.CnaeFiscalPrincipal + s.CnaeDescricao)
                                                                       .OrderByDescending(s => s.Count())
                                                                   select (new KeyValuePair<string, int>(tp3.Key, tp3.Count())))),

            Top3Atividades = from t3 in emps_ativas
                                .GroupBy(s => s.SetorProdutivo())
                                .OrderByDescending(s => s.Count())
                             select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(t3.Key,
                                                                     from tp3 in t3
                                                                         .Where(s => s.SetorProdutivo() == t3.Key)
                                                                         .GroupBy(s => s.CnaeFiscalPrincipal![..2])
                                                                         .OrderByDescending(s => s.Count())
                                                                         .Take(3)
                                                                     select (new KeyValuePair<string, int>(Dictionaries.CnaesSubClasses[tp3.Key], tp3.Count())))),

            Top3Atividades_Ano = from t3 in emps_ativas
                                .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                .GroupBy(s => s.SetorProdutivo())
                                .OrderByDescending(s => s.Count())
                                 select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(t3.Key,
                                                                         from tp3 in t3
                                                                             .Where(s => s.SetorProdutivo() == t3.Key)
                                                                             .GroupBy(s => s.CnaeFiscalPrincipal![..2])
                                                                             .OrderByDescending(s => s.Count())
                                                                             .Take(3)
                                                                         select (new KeyValuePair<string, int>(Dictionaries.CnaesSubClasses[tp3.Key], tp3.Count())))),

            Fiscal = from fc in emps_ativas
                                .GroupBy(s => s.RegimeFiscal())
                                .OrderByDescending(s => s.Count())
                     select (new KeyValuePair<string, int>(fc.Key, fc.Count())),

            Fiscal_Ano = from fca in emps_ativas
                            .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                            .GroupBy(s => s.RegimeFiscal())
                            .OrderByDescending(s => s.Count())
                         select (new KeyValuePair<String, int>(fca.Key, fca.Count())),

            Porte = from pte in emps_ativas
                            .GroupBy(s => s.PorteEmpresa)
                            .OrderByDescending(o => o.Count())
                    select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(pte.Key,
                                 from _fiscal in pte
                                     .GroupBy(s => s.RegimeFiscal())
                                     .OrderByDescending(s => s.Count())
                                 select (new KeyValuePair<string, int>(_fiscal.Key, _fiscal.Count())))),

            PorteS = from pte in emps_ativas
                            .GroupBy(s => s.PorteEmpresa)
                            .OrderByDescending(o => o.Count())
                     select (new KeyValuePair<string, int>(pte.Key, pte.Count())),

            Porte_Ano = from ptea in emps_ativas
                                .Where(s => s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                .GroupBy(s => s.PorteEmpresa)
                                .OrderByDescending(o => o.Count())
                        select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(ptea.Key,
                                     from _fiscal_a in ptea
                                         .GroupBy(s => s.RegimeFiscal())
                                         .OrderByDescending(s => s.Count())
                                     select (new KeyValuePair<string, int>(_fiscal_a.Key, _fiscal_a.Count())))),

            Simples = from sn in emps_ativas
                                .Where(s => s.OpcaoSimples == "S")
                                .GroupBy(s => s.PorteEmpresa)
                                .OrderByDescending(s => s.Count())
                      select (new KeyValuePair<string, int>(sn.Key, sn.Count())),

            Simples_Ano = from sna in emps_ativas
                                .Where(s => s.OpcaoSimples == "S" && s.DataInicioAtividade!.StartsWith(ano.ToString()))
                                .GroupBy(s => s.PorteEmpresa)
                                .OrderByDescending(s => s.Count())
                          select (new KeyValuePair<string, int>(sna.Key, sna.Count())),

            Idade = from age in emps_ativas
                                .GroupBy(d => DateTime.ParseExact(d.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).DateDiference())
                                .OrderBy(s => s.Key)
                    select (new KeyValuePair<string, int>(age.Key, age.Count())),

            Rotatividade = from rt in emps
                                .GroupBy(d => DateTime.ParseExact(d.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year.ToString())
                                .OrderByDescending(s => s.Key)
                                .Take(10)
                           select (new KeyValuePair<string, float>(rt.Key,
                                   Convert.ToSingle(rt.Where(s => s.SituacaoCadastral == "Baixada" && DateTime.ParseExact(s.DataSituacaoCadastral!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year.ToString() == rt.Key).Count()) /
                                   Convert.ToSingle(rt.Where(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year.ToString() == rt.Key).Count()) * 100)),

            EmpresasPorLocal = from lc in emps_bairro
                                  .GroupBy(s => s.Bairro)
                                  .OrderByDescending(o => o.Count())
                               select (new KeyValuePair<string, int>(lc.Key, lc.Count())),

            EmpresasNovasPorLocal = from nlc in emps_novas_bairro
                                        .GroupBy(s => s.Bairro)
                                        .OrderByDescending(o => o.Count())
                                    select (new KeyValuePair<string, int>(nlc.Key, nlc.Count())),

            TaxaCrescimentoSetorial = TaxaCrescimentoSetorial(emps_ativas)

        });
    }

    public async IAsyncEnumerable<MEmpresa> DoStoredProcedure(string param)
    {
        var cache = _memorycache;
        var cacheKey = $"DoStoredProcedure_{param!}";

        if (cache.TryGetValue(cacheKey, out IAsyncEnumerable<MEmpresa>? cachedEmpresas))
            await foreach (var item in cachedEmpresas!)
                yield return item;

        else
        {
            var empresas = _empresa.DoStoredProcedure(param);
            cachedEmpresas = empresas;
            cache.Set(cacheKey, cachedEmpresas, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(15)
            });

            await foreach (var item in empresas)
                yield return item;
        }
    }

    private static IEnumerable<KeyValuePair<string, int>> Quantitativo(IEnumerable<MEmpresa> list)
        => from st in list
                    .GroupBy(g => g.SituacaoCadastral)
                    .OrderByDescending(o => o.Count())
           select (new KeyValuePair<string, int>(st.Key, st.Count()));

    private static IEnumerable<KeyValuePair<int, IEnumerable<KeyValuePair<int, int>>>> MatrizEmpresarial(IEnumerable<MEmpresa> list)
    {
        var _emp_anos = list.OrderBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date);
        var _list = new List<KeyValuePair<int, IEnumerable<KeyValuePair<int, int>>>>();

        return from a in _emp_anos.GroupBy(s => DateTime.ParseExact(s.DataInicioAtividade!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year)
               select (new KeyValuePair<int, IEnumerable<KeyValuePair<int, int>>>(a.Key,
                   from e in _emp_anos
                                   .Where(s => s.SituacaoCadastral == "Baixada")
                                   .GroupBy(s => DateTime.ParseExact(s.DataSituacaoCadastral!, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year)
                                   .OrderBy(s => s.Count())
                   select (new KeyValuePair<int, int>(e.Key, e.Count()))));
    }

    private static IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> TaxaCrescimentoSetorial(IEnumerable<MEmpresa> list)
    {
        return from t in list
                            .Where(s => s.SetorProdutivo() != "Organizacoes Associativas" &&
                                        s.SetorProdutivo() != "Organismos internacionais" &&
                                        s.SetorProdutivo() != "Adm Publica")
                            .GroupBy(s => s.SetorProdutivo())
                            .OrderByDescending(s => s.Count())
               select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>(t.Key.ToString()!,
                            from a in list
                                        .Where(s => s.SetorProdutivo() == t.Key)
                                        .GroupBy(s => s.DataInicioAtividade!.Trim()[..4])
                                        .OrderByDescending(s => s.Key)
                                        .Take(11)
                            select (new KeyValuePair<string, int>(a.Key, a.Count()))));
    }

    public IAsyncEnumerable<MEmpresa> DoListAsync(string? municipio = null)
    {
        /*
        var cache = _memorycache;
        var cacheKey = $"DoListAsync_{municipio!}";

        if (cache.TryGetValue(cacheKey, out IAsyncEnumerable<MEmpresa>? cachedEmpresas))
        {
            await foreach (var item in cachedEmpresas!)
                yield return item;
        }
        else
        {
            var empresas = _empresa.DoListAsync(s => s.Municipio == municipio);

            // Adicione os itens ao cache sob demanda
            cachedEmpresas = empresas;
            cache.Set(cacheKey, cachedEmpresas, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(15)
            });

            await foreach (var item in empresas)
                yield return item;
        }*/
        return _empresa.DoListAsync(s => s.Municipio == municipio);
    }

    public async Task<RCharts> DoReportToChartAsync(REmpresas report)
    {
        return new RCharts(
            Municipio: report.Municipio!,
            Rotatividade: await Task.Run(() =>
            {
                string _rotatividade_emp = string.Empty;
                foreach (var x in report.Rotatividade!.OrderBy(s => s.Key))
                    _rotatividade_emp += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), Math.Round(x.Value, 1).ToString().Replace(',', '.'));

                return _rotatividade_emp.Length > 0 ? _rotatividade_emp[..^1] : _rotatividade_emp;
            }),

            NovasMes: await Task.Run(() =>
            {
                string[] _novas_mes = new string[2];
                foreach (var m in report.NovasEmpresas_Ano!)
                {
                    _novas_mes[0] += string.Format(@"{0},", m.Value);
                    _novas_mes[1] += string.Format(@"`{0}`,", m.Key);
                }
                return _novas_mes;
            }),

            BaixasMes: await Task.Run(() =>
            {
                string[] _baixas_mes = new string[2];
                foreach (var m in report.Baixas_Ano!)
                {
                    _baixas_mes[0] += string.Format(@"{0},", m.Value);
                    _baixas_mes[1] += string.Format(@"`{0}`,", m.Key);
                }
                return _baixas_mes;
            }),

            MatrizFilial: await Task.Run(() =>
            {
                string[] _matriz = new string[2];
                foreach (var item in report.MatrizFilial!)
                {
                    _matriz[0] += string.Format(@"{0},", item.Value);
                    _matriz[1] += string.Format(@"`{0}`,", item.Key);
                }
                return _matriz;
            }),

            MatrizFilial_Ano: await Task.Run(() =>
            {
                string[] _matriz_ano = new string[2];
                foreach (var item in report.MatrizFilial_Ano!)
                {
                    _matriz_ano[0] += string.Format(@"{0},", item.Value);
                    _matriz_ano[1] += string.Format(@"`{0}`,", item.Key);
                }
                return _matriz_ano;
            }),

            Fiscal: await Task.Run(() =>
            {
                string[] _fiscal = new string[2];
                foreach (var fiscal in report.Fiscal!)
                {
                    _fiscal[0] += string.Format(@"{0},", fiscal.Value);
                    _fiscal[1] += string.Format(@"`{0}`,", fiscal.Key);
                }
                return _fiscal;
            }),

            Fiscal_Ano: await Task.Run(() =>
            {
                string[] _fiscal_ano = new string[2];
                foreach (var fiscal in report.Fiscal_Ano!)
                {
                    _fiscal_ano[0] += string.Format(@"{0},", fiscal.Value);
                    _fiscal_ano[1] += string.Format(@"`{0}`,", fiscal.Key);
                }
                return _fiscal_ano;
            }),

            PorteFiscal: await Task.Run(() =>
            {
                var portefiscal = new string[3, 3];

                for (int i = 0; i < portefiscal!.GetLength(0); i++)
                    for (int j = 0; j < portefiscal.GetLength(1); j++)
                        portefiscal[i, j] = "0";

                int a = 0;
                foreach (var item in report.Porte!.Where(s => s.Key != "N/I").OrderBy(s => s.Key.Length))
                {

                    int b = 0;

                    if (item.Key == "Demais" || item.Key == "EPP")
                        b = 2;

                    foreach (var subitem in item.Value)
                    {
                        if (item.Key == "Demais" || item.Key == "EPP")
                        {
                            portefiscal![a, b] = string.Format(@"{0}", subitem.Value);
                            b--;
                        }
                        else
                        {
                            portefiscal![a, b] = string.Format(@"{0}", subitem.Value);
                            b++;
                        }
                    }
                    a++;
                }

                var _return = new string[3];

                _return[0] = $"{portefiscal[0, 0]},{portefiscal[1, 0]},{portefiscal[2, 0]}";
                _return[1] = $"{portefiscal[0, 1]},{portefiscal[1, 1]},{portefiscal[2, 1]}";
                _return[2] = $"{portefiscal[0, 2]},{portefiscal[1, 2]},{portefiscal[2, 2]}";

                return _return;
            }),

            PorteFiscalAno: await Task.Run(() =>
            {
                var portefiscalano = new string[3, 3];
                for (int i = 0; i < portefiscalano.GetLength(0); i++)
                    for (int j = 0; j < portefiscalano.GetLength(1); j++)
                        portefiscalano[i, j] = "0";

                var a = 0;
                foreach (var item in report.Porte_Ano!.OrderBy(s => s.Key.Length))
                {

                    int b = 0;

                    if (item.Key == "Demais" || item.Key == "EPP")
                        b = 2;

                    foreach (var subitem in item.Value)
                    {
                        if (item.Key == "Demais" || item.Key == "EPP")
                        {
                            portefiscalano![a, b] = string.Format(@"{0}", subitem.Value);
                            b--;
                        }
                        else
                        {
                            portefiscalano![a, b] = string.Format(@"{0}", subitem.Value);
                            b++;
                        }
                    }
                    a++;
                }
                var _return = new string[3];

                _return[0] = $"{portefiscalano[0, 0]},{portefiscalano[1, 0]},{portefiscalano[2, 0]}";
                _return[1] = $"{portefiscalano[0, 1]},{portefiscalano[1, 1]},{portefiscalano[2, 1]}";
                _return[2] = $"{portefiscalano[0, 2]},{portefiscalano[1, 2]},{portefiscalano[2, 2]}";

                return _return;
            }),

            LabelPorteFiscal: new string[3] { "MEI", "Simples", "Outros" },

            Maturidade: await Task.Run(() =>
            {
                string _emp_faixa_etaria = string.Empty;
                foreach (var x in report.Idade!)
                    _emp_faixa_etaria += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

                return _emp_faixa_etaria.Length > 0 ? _emp_faixa_etaria[..^1] : _emp_faixa_etaria;
            }),

            Setores: await Task.Run(() =>
            {
                var _setores = new string[2];
                foreach (var item in report.Setores!)
                {
                    _setores![0] += string.Format(@"`{0}`,", item.Key);
                    _setores![1] += string.Format(@"{0},", item.Value);
                }
                return _setores;
            }),

            SetoresAno: await Task.Run(() =>
            {
                var _setoresano = new string[2];
                foreach (var item in report.Setores_Ano!)
                {
                    _setoresano![0] += string.Format(@"`{0}`,", item.Key);
                    _setoresano![1] += string.Format(@"{0},", item.Value);
                }
                return _setoresano;
            }),

            //Local: Array.Empty<string>(),

            CrescimentoSetorial: await Task.Run(() =>
            {
                var _tx_setorial = string.Empty;
                var _series_names = string.Empty;
                var _crescimentosetorial = new string[2];

                foreach (var item in report.TaxaCrescimentoSetorial!)
                {
                    _series_names += string.Format(@"`{0}`,", item.Key);
                    var _nomes = string.Format(@"{{name:`{0}`,data:[", item.Key);
                    var _datas = string.Empty;
                    var _acumulador = report.Setores!.Where(s => s.Key == item.Key).Sum(s => s.Value) - item.Value.Sum(s => s.Value);
                    foreach (var sub in item.Value.OrderBy(s => s.Key))
                    {
                        _acumulador += sub.Value;
                        _datas += string.Format(@"{{x:`{0}`, y:{1}}},", sub.Key, _acumulador);
                    }
                    _tx_setorial += string.Format(@"{0}{1}]}},", _nomes, _datas[..^1]);
                }
                _crescimentosetorial![1] = _tx_setorial!.Length > 0 ? _tx_setorial![..^1] : _tx_setorial;
                _crescimentosetorial![0] = _series_names!.Length > 0 ? _series_names[..^1] : _series_names;
                return _crescimentosetorial;
            }),

            Setores_Controle: from s in report.Setores select (new string(s.Key))
        );

    }

    public async Task<IEnumerable<string>> DoListMunicipiosEstadoSP()
    {
        try
        {
            var _file = @"../../files/municipio-estado-sp.csv";

            var _return = new List<string>();

            using (var reader = new StreamReader(_file, Encoding.GetEncoding("ISO-8859-1")))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var fields = line!.Split(',');

                    foreach (var item in fields)
                        _return.Add(item);
                }
            }

            return _return;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}