using IDN.Services.Cnae.View;
using IDN.Services.Empresa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class EmpresasController : ControllerBase
{
    private readonly IServiceEmpresa _empresas;
    private readonly ILogger<EmpresasController> _log;

    public EmpresasController(IServiceEmpresa serviceEmpresa,
        ILogger<EmpresasController> log)
    {
        _log = log;
        _empresas = serviceEmpresa;
    }

    /// <summary>
    /// Retorna empresas agrupadas por atividade primária
    /// </summary>
    /// <param name="s">1531902 or 15</param>
    /// <param name="m">jau</param>
    /// <returns>Lista com a quantidade de empresas ativas agrupadas por atividade primária do CNAE</returns>
    [HttpGet("empresas/cnae/{s}/{m}")]
    public async Task<IActionResult> DoListBySegmento([FromRoute] string s, [FromRoute] string m)
    {
        var _token = $"_list_empresas_segmento_{s}{m}";
        var _list = new List<KeyValuePair<string, int>>();
        var _segmento = new List<string>();
        await foreach (var item in _empresas.DoListAsync(e => e.Municipio == m.ToUpper() && e.CnaeFiscalPrincipal!.StartsWith(s) && e.SituacaoCadastral == "02", _token))
        {
            _segmento.Add(new string($"{item.CnaeFiscalPrincipal} {item.CnaeDescricao}"));
        }

        _log.LogInformation(_token);
        return Ok(from emp in _segmento
                                .GroupBy(g => g)
                                .OrderByDescending(o => o.Count())
                  select (new KeyValuePair<string, int>(emp.Key, emp.Count())));
    }


}