using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Razor.Pages.UF;

public class SPModel : PageModel
{
    private readonly IMongoDB<REmpresas>? _mongoDB;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }

    public IEnumerable<string>? ListaMunicipios { get; set; }

    public SPModel()
    { _mongoDB = Factory<REmpresas>.NewDataMongoDB(); }

    public async Task OnGetAsync()
    { 
        var _list = new List<string>();
        foreach(var item in await _mongoDB!.DoListAsync(null))
            _list.Add(item.Municipio!);       

        ListaMunicipios = _list;      
    }
}
