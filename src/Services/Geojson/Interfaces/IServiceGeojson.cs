using System.Net.Http.Json;
using IDN.Core.Geojson.Models;
using IDN.Services.Geojson.View;
using Newtonsoft.Json;

namespace IDN.Services.Geojson.Interfaces;

public interface IServiceGeojson
{
    Task<VGeojson> ReadFileGeojson();
    Task<string> ReadFileGeojsonAsync();
    Task<VGeojson> DoGeojsonAsync(string? param = null);
    Task<MGeojson> DoGeojsonToDBAsync(VGeojson obj);    
}