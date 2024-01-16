using IDN.Data.Helpers;
using IDN.Services.Empresa.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages.Maps;

public class IndexModel : PageModel
{

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public IEnumerable<REmpresas>? LReports { get; set; }
    public string? Empresasativas { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Setores { get; set; } = new List<KeyValuePair<string, string>>();
    public async Task<ActionResult> OnGetAsync(string? m)
    {
        if (string.IsNullOrEmpty(m))
            return RedirectToPage("/Index");

        var param = m?.ToLower();
        Cidade = param;
        var param2 = param?.ToUpper();
        await LoadData(param2);

        return Page();
    }

    public async Task LoadData(string? param)
    {
        var _mongoE = Factory<REmpresas>.NewDataMongoDB();
        var _filter = Builders<REmpresas>.Filter.Eq(e => e.Municipio, param);
        LReports = await _mongoE.DoListAsync(_filter);

        foreach (var report in LReports)
        {
            foreach (var q in report.Quantitativo!.Where(s => s.Key == "Ativa"))
            {
                Empresasativas = $"Empresas ativas: {q.Value}";

                Setores = from s in report.Setores
                          select (new KeyValuePair<string, string>(s.Key, $"({s.Value * 100 / q.Value}%)"));
            }
        }
    }
}
