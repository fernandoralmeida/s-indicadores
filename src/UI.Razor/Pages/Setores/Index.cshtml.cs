using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Razor.Pages.Setores;

public class IndexModel : PageModel
{

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }

    public IndexModel()
    { }

    public void OnGetAsync()
    { }
}
