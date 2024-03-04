using System.Net.Http.Json;
using IDN.Core.Geojson.Models;
using IDN.Services.Geojson.View;
using Newtonsoft.Json;

namespace IDN.Services.Geojson.Interfaces;

public interface IServiceGeojson
{
    Task<VGeojson> ReadFileGeojson();
    Task<IEnumerable<VFeatures>> NewReadFileGeojsonAsync();
    Task<string> ReadFileGeojsonAsync();
    Task<VGeojson> DoGeojsonAsync(string? param = null);
    Task<MGeojson> DoGeojsonToDBAsync(VGeojson obj);
    Task<VGeojson> DoGeojson(string? municipio = null);
    Task<IEnumerable<VFeatures>> DoListGeojson(string[]? municipios = null);
    Task<string> GeoJsonUF();
}