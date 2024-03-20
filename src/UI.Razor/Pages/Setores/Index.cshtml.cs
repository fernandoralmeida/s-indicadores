using IDN.Services.Cnae.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Core.Helpers;

namespace UI.Razor.Pages.Setores;

public class IndexModel : PageModel
{
    private readonly IServiceCnae _cnaes;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }
    public string? UrlAPI { get; set; } = "/api/v1/geojson/";

    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<string>>>>>? Setore_Segmentos_Cnaes { get; set; }

    public IndexModel(IServiceCnae cnae)
    { _cnaes = cnae; }

    public async Task OnGetAsync()
    {
        var _list = new List<(string cnae, string desc, string setor)>();
        foreach (var item in await _cnaes.DoListAsync())
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

    public async Task OnPostAsync()
    {
        var _list = new List<(string cnae, string desc, string setor)>();

        var _test_search = Search?.ToLower().NormalizeText();

        var _list_cnae = Search == null ?
                            await _cnaes.DoListAsync() :
                            await _cnaes.DoListAsync(s => s.Codigo!.StartsWith(Search) ||
                                                            s.Descricao!                                                                                                                       
                                                            .ToLower()                                                         
                                                            .Contains(_test_search!));

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
}
