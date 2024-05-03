using IDN.Core.Helpers;
using IDN.Data.Helpers;
using IDN.Data.Interface;
using IDN.Services.Empresa.Records;
using IDN.Services.Geojson.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace UI.Razor.Pages;

public class IndexModel : PageModel
{
    private readonly IMongoDB<REmpresas>? _mongoDB;
    private readonly IMongoDB<VFeatures> _geojson;

    [BindProperty(SupportsGet = true)]
    public string? Cidade { get; set; }
    public string? GeoCode { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesAdministrativas { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesGovernoSP { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? RegioesMetropolitanasSP { get; set; }
    public IEnumerable<KeyValuePair<string, string>>? AglomeradosUrbanosSP { get; set; }

    public IndexModel()
    {
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _geojson = Factory<VFeatures>.NewDataMongoDB();
    }

    public void OnGet()
    {
        var _list = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.MacroRegioesSP())
            _list.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_rg = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.RegioesGovernoSP())
            _list_rg.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_rm = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.RegioesMetropolitanasSP())
            _list_rm.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        var _list_au = new List<KeyValuePair<string, string>>();
        foreach (var item in Regions.AglomeradosUrbanosSP())
            _list_au.Add(new(item, item.NormalizeText().ToLower().Replace(" ", "-")));

        RegioesAdministrativas = _list;
        RegioesGovernoSP = _list_rg;
        RegioesMetropolitanasSP = _list_rm;
        AglomeradosUrbanosSP = _list_au;
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Cidade))
            return Page();

        var _cities = Cidade?.Split(',');

        if (_cities?.Length > 1)
        {
            Cidade = string.Empty;
            GeoCode = string.Empty;

            foreach (var city in _cities!)
            {
                var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, city);
                foreach (var _geo in await _geojson.DoListAsync(__city))
                {
                    GeoCode += $"{_geo.Properties!.Geocode},";
                    Cidade += $"{_geo.Properties!.Name},";
                }
            };
            Cidade = Cidade[..^1];
            GeoCode = GeoCode[..^1];
        }
        else
        {
            var _param = Cidade?.ToLower();
            var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _param);
            foreach (var _geo in await _geojson.DoListAsync(__city))
                GeoCode = _geo.Properties!.Geocode;
        }

        return RedirectToPage("/Maps/Index", new { m = GeoCode });
    }
}
