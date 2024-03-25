using IDN.Core.Helpers;
using IDN.Services.Cnae.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class CnaeController : ControllerBase
{
    private readonly IServiceCnae _cnaes;

    public CnaeController(IServiceCnae cnaes)
    {
        _cnaes = cnaes;
    }

    [HttpGet("cnae/{c}")]
    public async Task<IActionResult> DoListParam([FromRoute] string c)
    {
        var _list = await _cnaes.DoListAsync(s => s.Codigo == c);
        return Ok(_list);
    }

    [HttpGet("cnae/list")]
    public async Task<IActionResult> DoList()
    {
        return Ok(await _cnaes.DoListAsync());

    }

    [HttpGet("segmento/list/{c}")]
    public async Task<IActionResult> DoListStartWith([FromRoute] string c)
    {
        var _start = c.Length > 1 ? c[..2] : c;
        var _list = await _cnaes.DoListAsync(s => s.Codigo!.StartsWith(_start));
        return Ok(_list);
    }

    [HttpGet("setores/{c?}")]
    public async Task<IActionResult> DoSetores([FromRoute] string? c)
    {
        var _list = new List<(string cnae, string desc, string setor)>();

        var _test_search = c?.ToLower().NormalizeText();

        var _list_cnae = c == null ?
                            await _cnaes.DoListAsync() :
                            await _cnaes.DoListAsync(s => s.Codigo!.StartsWith(c) ||
                                                            s.Descricao!
                                                            .ToLower()
                                                            .Contains(_test_search!));

        foreach (var item in _list_cnae)
            _list.Add(new(item.Codigo!, item.Descricao!, Dictionaries.SetorProdutivo[item.Codigo![..2]]));


        return Ok(from st in _list
                                .GroupBy(g => g.setor)

                                select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<string>>>>(st.Key,
                                    from sg in st
                                                .Where(s => s.cnae.StartsWith(s.cnae[..2]))
                                                .GroupBy(g => g.cnae[..2])
                                select (new KeyValuePair<string, IEnumerable<string>>($"{sg.Key[..2]} {Dictionaries.CnaesSubClasses[sg.Key[..2]]}",
                                    from ce in _list
                                                    .Where(s => s.cnae!.StartsWith(sg.Key[..2]))
                                    select new string($"{ce.cnae} {ce.desc}")
                                    )))));
    }


}