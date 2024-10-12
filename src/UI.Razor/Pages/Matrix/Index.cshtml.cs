
using IDN.Core.Empresa.Models;
using IDN.Services.Empresa.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Razor.Pages.Matrix;

public class IndexModel : PageModel
{
    private readonly IServiceEmpresa? _empresa;

    public IEnumerable<KeyValuePair<int, IEnumerable<(int, int, int)>>>? MatrizEmpresarial { get; set; }

    public IndexModel(IServiceEmpresa empresa)
    {
        _empresa = empresa;
    }

    public async Task OnGetAsync(string? m, string? z, string? c)
    {
        var _list = new List<MEmpresa>();
        await foreach (var item in _empresa!.DoListAsync(s => s.Municipio == m!.ToUpper()))
            _list.Add(item);

        MatrizEmpresarial = await _empresa.DoMatrizEmpresarial(_list);
    }
}
