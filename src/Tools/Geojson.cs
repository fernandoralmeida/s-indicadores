using System.Diagnostics;
using IDN.Data.Helpers;
using IDN.Services.Geojson.Services;
using IDN.Services.Geojson.View;

namespace IDN.Tools;

public class Geojson
{
    private static readonly ServiceGeojson _geojson = new();

    public static async Task GoeCidades()
    {
        var _timer = new Stopwatch();
        _timer.Start();
        Console.WriteLine("Geocodes");
        var _mongoDB = Factory<VFeatures>.NewDataMongoDB();
        Console.WriteLine("Reading .geojson");
        var _features = await _geojson.NewReadFileGeojsonAsync();

        Console.WriteLine("Insert on MongoDB");
        await _mongoDB.InsertManyAsync(_features);

        _timer.Stop();
        Console.WriteLine($"Insertions :{_features.Count()}");
        Console.WriteLine($"Timer: {_timer.Elapsed:hh\\:mm\\:ss\\.fff}");
    }

}