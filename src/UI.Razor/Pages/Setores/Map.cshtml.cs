using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Razor.Pages.Setores;

public class MapModel : PageModel
{

    [BindProperty(SupportsGet = true)]
    public string? CNAE { get; set; }
    public void OnGet(string? m)
    {
        CNAE = m;
    }
}