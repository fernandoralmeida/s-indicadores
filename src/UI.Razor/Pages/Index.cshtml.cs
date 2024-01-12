using Microsoft.AspNetCore.Mvc.RazorPages;
using IDN.Services.Municipio.Interfaces;
using IDN.Services.Municipio.View;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace UI.Razor.Pages;

public class IndexModel : PageModel
{
    public readonly IServiceMunicipio _municipio;
    public string? TimerShow { get; set; }
    public string? Municipio { get; set; }
    public IEnumerable<VMunicipio>? Municipios { get; set; }
    public SelectList? ListaMunicipios { get; set; }
    public IndexModel(IServiceMunicipio municipio)
    {
        _municipio = municipio;
    }

    public IActionResult OnGetAsync()
    {
        //var _list = await _municipio.DoMicroRegiaoJauAsync();
        //ListaMunicipios = new SelectList(_list.OrderBy(o => o.Descricao), nameof(VMunicipio.Descricao), nameof(VMunicipio.Descricao), null);
        return RedirectToPage("/Empresas/Map");
    }
}
