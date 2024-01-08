using IDN.Services.Geojson.Services;

namespace IDN.Tools;

public class Geojson
{
    private readonly ServiceGeojson _geojson = new();

    public async Task GoeCidades()
    {
        var _cidades = new List<string> { "Jau", "Jau", "Jau", "Jau", "Jau", "Jau", "Jau" };
        var _result = await _geojson.ReadFileGeojson(_cidades);
        foreach (var item in _result.Features!)
            Console.WriteLine(item);
    }

}