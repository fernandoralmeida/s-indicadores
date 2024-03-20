using IDN.Services.Geojson.Interfaces;
using IDN.Services.Geojson.View;
using IDN.Core.Helpers;
using IDN.Core.Geojson.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IDN.Data.Helpers;
using MongoDB.Driver;
using IDN.Services.Empresa.Records;

namespace IDN.Services.Geojson.Services;

public class ServiceGeojson : IServiceGeojson
{
    public async Task<VGeojson> DoGeojson(string? municipio = null)
    {
        var _mongoDB = Factory<VFeatures>.NewDataMongoDB();
        //var _mongoDBE = Factory<REmpresas>.NewDataMongoDB();
        var _filter = municipio == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, municipio);
        //var _m2 = municipio! == null ? null : municipio.ToUpper();
        //var _efilter = _m2 == null ? null : Builders<REmpresas>.Filter.Eq(e => e.Municipio, _m2);
        //var _feature = new VFeatures();
        //var _features = new List<VFeatures>();
        var _features = await _mongoDB.DoListAsync(_filter);
        var _geojson = new VGeojson
        {
            Type = "FeatureCollection",
            Max = _features.Sum(s => s.Properties!.Empresas),
            Features = _features
        };
        /*
        var _cities = await _mongoDBE.DoListAsync(_efilter);

        foreach (var r in await _mongoDB.DoListAsync(_filter))
        {
            foreach (var i in _cities.Where(s => s.Municipio!.ToLower() == r.Properties!.Name!.ToLower()))
            {
                _feature = r;
                foreach (var q in i.Quantitativo!.Where(s => s.Key == "Ativa"))
                {
                    var _p = (i.Setores!.FirstOrDefault().Value * 100) / q.Value;
                    _feature.Properties!.Empresas = q.Value;
                    _feature.Properties!.Setor = $"{i.Setores!.FirstOrDefault().Key} ({_p}%)";
                }
                _features.Add(_feature);
            }
        }

        _geojson.Features = _features;
        */
        return _geojson;
    }

    public async Task<IEnumerable<VFeatures>> DoListGeojson(string[]? municipios = null)
    {
        var _mongoDB = Factory<VFeatures>.NewDataMongoDB();
        var _features = new List<VFeatures>();
        foreach (var municipio in municipios!)
        {
            var _m = municipio.Trim();
            var _filter = _m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _m);
            foreach (var item in await _mongoDB.DoListAsync(_filter))
                _features.Add(item);
        }
        return _features;
    }

    public async Task<VGeojson> DoGeojsonAsync(string? param = null)
    {
        var _geojson = new VGeojson();
        var _features = new List<VFeatures>();

        var _path = @"/home/dbn/sources/s-indicadores/files/geojson-estado-sp.json";

        string jsonString = await File.ReadAllTextAsync(_path);

        // Deserializa o JSON para um objeto JObject
        JObject? jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);
        // Acessando a lista de features
        JArray features = (JArray)jsonObject!["features"]!;

        // Iterando sobre cada feature
        foreach (JObject feature in features.Cast<JObject>()
                                            .Where(s => s!["properties"]!["name"]!
                                            .ToString()
                                            .ToLower().NormalizeText() == param!.ToLower().NormalizeText()))
        {
            var _feature = new VFeatures
            {
                // Acessando feature
                Type = (string)feature!["type"]!,
                Properties = new VProperties()
                {
                    Name = feature!["properties"]!["name"]!.ToString(),
                    Geocode = feature!["properties"]!["geocode"]!.ToString()
                }
            };


            // Acessando o tipo de geometria
            string tipoGeometria = (string)feature!["geometry"]!["type"]!;

            var _geometry = new VGeometry
            {
                Type = tipoGeometria
            };

            var _coordinates = new List<List<List<List<double>>>>();

            // Acessando as coordenadas da geometria (para MultiPolygon, precisa tratar o loop interno)
            if (tipoGeometria == "MultiPolygon")
            {
                JArray coordinates = (JArray)feature!["geometry"]!["coordinates"]!;

                foreach (JArray polygon in coordinates.Cast<JArray>())
                {
                    var _poly = new List<List<double>>();
                    foreach (JArray point in polygon.Cast<JArray>())
                    {
                        foreach (var p in point)
                        {
                            var _point = new List<double>();
                            foreach (var _x_y in p.Select(s => (double)s))
                            {
                                _point.Add(_x_y);
                            }
                            _poly.Add(_point);
                        }
                    }
                    _coordinates.Add(new List<List<List<double>>>() { _poly });
                }
                _geometry.Coordinates = _coordinates;
            }

            _feature.Geometry = _geometry;
            _features.Add(_feature);
        }

        _geojson.Type = "FeatureCollection";
        _geojson.Features = _features;

        return _geojson;
    }

    public Task<MGeojson> DoGeojsonToDBAsync(VGeojson obj)
    {
        return Task.Run(() =>
        {
            var _feature = new MFeatures();

            foreach (var item in obj.Features!)
            {
                _feature.Id = Guid.NewGuid();
                _feature.Type = item.Type;

                //geometry
                _feature.Geometry = new MGeometry()
                {
                    Id = Guid.NewGuid(),
                    Type = item.Geometry!.Type
                };
                var _list_coord = new List<MCoordinates>();
                foreach (var geo in item.Geometry!.Coordinates!)
                {
                    foreach (var poly in geo)
                    {
                        foreach (var point in poly)
                        {
                            //numeros são fixos, long e lat
                            var _coord = new MCoordinates()
                            {
                                Id = Guid.NewGuid(),
                                X = point.Select(s => s).First(),
                                Y = point.Select(s => s).Last(),
                            };
                            _list_coord.Add(_coord);
                        }
                    }
                }
                _feature.Geometry.Coordinates = _list_coord;

                //properties
                _feature.Properties = new MProperties()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Properties!.Name,
                    Geocode = item.Properties!.Geocode
                };

            }

            return new MGeojson()
            {
                Type = "FeatureCollection",
                Features = new List<MFeatures>() { _feature }
            };
        });
    }

    public async Task<IEnumerable<VFeatures>> NewReadFileGeojsonAsync()
    {
        var _features = new List<VFeatures>();
        var _mongoDBE = Factory<REmpresas>.NewDataMongoDB();

        var _file = string.Empty;

        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            _file = @"C:\\Users\\User\\OneDrive\\Dev\\BC250_2017_Municipio_A.json";
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            _file = @"/home/dbn/sources/s-indicadores/files/BC250_2017_Municipio_A.json";
        }

        string jsonString = await File.ReadAllTextAsync(_file);

        // Deserializa o JSON para um objeto JObject
        JObject? jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

        // Acessando a lista de features
        JArray features = (JArray)jsonObject!["features"]!;

        var _cities = await _mongoDBE.DoListAsync(null);
        // Iterando sobre cada feature
        // Geocodigo com inicio 35 = Estado de São Paulo    
        foreach (JObject feature in features.Cast<JObject>()
                                            .Where(s => s!["properties"]!["geocodigo"]!.ToString().StartsWith("35")))
        {

            var _feature = new VFeatures
            {
                // Acessando feature
                Type = (string)feature!["type"]!,
                Properties = new VProperties()
                {
                    Name = feature!["properties"]!["nome"]!.ToString().NormalizeText().ToLower(),
                    Geocode = feature!["properties"]!["geocodigo"]!.ToString()
                }

            };

            if (feature!["properties"]!["nome"]!.ToString().NormalizeText().ToLower() == "florinia")
            {
                _feature.Properties.Name = "florinea";
            }

            if (feature!["properties"]!["nome"]!.ToString().NormalizeText().ToLower() == "mogi guacu")
            {
                _feature.Properties.Name = "mogi-guacu";
            }

            foreach (var i in _cities.Where(s => s.Municipio!.ToLower() == _feature.Properties!.Name!.ToLower()))
            {
                foreach (var q in i.Quantitativo!.Where(s => s.Key == "Ativa"))
                {
                    var _p = (i.Setores!.FirstOrDefault().Value * 100) / q.Value;
                    _feature.Properties!.Empresas = q.Value;
                    _feature.Properties!.Setor = $"{i.Setores!.FirstOrDefault().Key} ({_p}%)";
                }
            }

            // Acessando o tipo de geometria
            string tipoGeometria = (string)feature!["geometry"]!["type"]!;

            var _geometry = new VGeometry
            {
                Type = tipoGeometria
            };

            var _coordinates = new List<List<List<List<double>>>>();

            // Acessando as coordenadas da geometria (para MultiPolygon, precisa tratar o loop interno)
            if (tipoGeometria == "MultiPolygon")
            {
                JArray coordinates = (JArray)feature!["geometry"]!["coordinates"]!;

                foreach (JArray polygon in coordinates.Cast<JArray>())
                {
                    var _poly = new List<List<double>>();
                    foreach (JArray point in polygon.Cast<JArray>())
                    {
                        foreach (var p in point)
                        {
                            var _point = new List<double>();
                            foreach (var _x_y in p.Select(s => (double)s))
                            {
                                _point.Add(_x_y);
                            }
                            _poly.Add(_point);
                        }
                    }
                    _coordinates.Add(new List<List<List<double>>>() { _poly });
                }
                _geometry.Coordinates = _coordinates;
            }

            _feature.Geometry = _geometry;
            _features.Add(_feature);
        }

        return _features;
    }

    public async Task<VGeojson> ReadFileGeojson()
    {
        var _geojson = new VGeojson();
        var _features = new List<VFeatures>();

        //var _file = @"BC250_2017_Municipio_A.json";
        var _file = @"/home/dbn/sources/s-indicadores/files/BC250_2017_Municipio_A.json";

        string jsonString = await File.ReadAllTextAsync(_file);

        // Deserializa o JSON para um objeto JObject
        JObject? jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

        // Acessando a lista de features
        JArray features = (JArray)jsonObject!["features"]!;

        // Iterando sobre cada feature
        // Geocodigo com inicio 35 = Estado de São Paulo
        foreach (JObject feature in features.Cast<JObject>()
                                            .Where(s => s!["properties"]!["geocodigo"]!.ToString().StartsWith("35")))
        {
            var _feature = new VFeatures
            {
                // Acessando feature
                Type = (string)feature!["type"]!,
                Properties = new VProperties()
                {
                    Name = feature!["properties"]!["nome"]!.ToString(),
                    Geocode = feature!["properties"]!["geocodigo"]!.ToString()
                }
            };

            // Acessando o tipo de geometria
            string tipoGeometria = (string)feature!["geometry"]!["type"]!;

            var _geometry = new VGeometry
            {
                Type = tipoGeometria
            };

            var _coordinates = new List<List<List<List<double>>>>();

            // Acessando as coordenadas da geometria (para MultiPolygon, precisa tratar o loop interno)
            if (tipoGeometria == "MultiPolygon")
            {
                JArray coordinates = (JArray)feature!["geometry"]!["coordinates"]!;

                foreach (JArray polygon in coordinates.Cast<JArray>())
                {
                    var _poly = new List<List<double>>();
                    foreach (JArray point in polygon.Cast<JArray>())
                    {
                        foreach (var p in point)
                        {
                            var _point = new List<double>();
                            foreach (var _x_y in p.Select(s => (double)s))
                            {
                                _point.Add(_x_y);
                            }
                            _poly.Add(_point);
                        }
                    }
                    _coordinates.Add(new List<List<List<double>>>() { _poly });
                }
                _geometry.Coordinates = _coordinates;
            }

            _feature.Geometry = _geometry;
            _features.Add(_feature);
        }

        _geojson.Type = "FeatureCollection";
        _geojson.Features = _features;

        return _geojson;
    }

    public async Task<string> ReadFileGeojsonAsync()
    {
        var _file = @"/home/dbn/sources/s-indicadores/files/geojson-estado-sp.json";

        return await File.ReadAllTextAsync(_file);
    }

    public async Task<string> GeoJsonUF()
    {
        var _path = @"/home/dbn/sources/s-indicadores/files/geojson-uf.json";

        string jsonString = await File.ReadAllTextAsync(_path);

        return jsonString;
    }
}