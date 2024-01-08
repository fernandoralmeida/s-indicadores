using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Razor.Pages.Empresas;

public partial class MapModel : PageModel
{
    public MapModel()
    { }

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public void OnGetAsync(string? m)
    {
        Cidade = m;
    }
}
