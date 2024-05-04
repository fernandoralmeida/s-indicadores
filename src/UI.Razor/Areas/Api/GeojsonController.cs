using IDN.Services.Geojson.Interfaces;
using IDN.Services.Empresa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IDN.Data.Helpers;
using IDN.Services.Geojson.View;
using IDN.Services.Cnae.Interfaces;
using IDN.Services.Empresa.Records;
using IDN.Core.Helpers;
using MongoDB.Driver;
using IDN.Data.Interface;
using MongoDB.Bson;

namespace UI.Razor.Areas.Api;

[Route("api/v1")]
[ApiController]
public class GeojsonController : ControllerBase
{
    private readonly IServiceGeojson? _geocode;
    private readonly IServiceEmpresa? _empresas;
    private readonly IServiceCnae? _cnaes;
    private readonly IMongoDB<REmpresas>? _mongoDB;
    private readonly IMongoDB<VFeatures>? _geojsonfeatures;
    public GeojsonController(IServiceGeojson geocode,
                                IServiceEmpresa empresas,
                                IServiceCnae cnaes)
    {
        _geocode = geocode;
        _empresas = empresas;
        _cnaes = cnaes;
        _mongoDB = Factory<REmpresas>.NewDataMongoDB();
        _geojsonfeatures = Factory<VFeatures>.NewDataMongoDB();
    }

    [HttpGet("geojson/{m?}")]
    public async Task<IActionResult> GetGeojson([FromRoute] string? m)
    {
        var param = m ?? null;

        var _cities = param?.Split(',');

        if (_cities?.Length > 0)
        {
            var _features = await _geocode!.DoListGeojson(_cities);

            return Ok(new VGeojson
            {
                Type = "FeatureCollection",
                Max = _features.Sum(s => s.Properties!.Empresas),
                Features = _features
            });
        }
        else
            return Ok(await _geocode!.DoGeojson(param));
    }

    [HttpGet("geojson/uf")]
    public async Task<IActionResult> GetGeojsonUF()
    {
        return Ok(await _geocode!.GeoJsonUF());
    }

    // [HttpGet("geojson/cnae/{n}/{m?}")]
    // public async Task<IActionResult> ListByCNAE([FromRoute] string n, [FromRoute] string? m)
    // {
    //     try
    //     {
    //         var _list = new List<MEmpresa>();

    //         if (string.IsNullOrEmpty(m))
    //             await foreach (var item in _empresas!.DoListAsync(s => s.CnaeFiscalPrincipal == n && s.SituacaoCadastral == "02", $"cnae_{n}_{m}"))
    //                 _list.Add(item);

    //         else
    //             await foreach (var item in _empresas!.DoListAsync(s => s.CnaeFiscalPrincipal == n && s.Municipio == m.ToUpper() && s.SituacaoCadastral == "02", $"cnae_{n}_{m}"))
    //                 _list.Add(item);

    //         var _list_report = new List<REmpresas>();
    //         var _list_m = _list.GroupBy(s => s.Municipio).OrderByDescending(s => s.Count());

    //         foreach (var item in _list_m.Where(s => s.Any(s => s.SituacaoCadastral == "02")))
    //         {
    //             _list_report.Add(await _empresas.DoReportEmpresasAsync(item));
    //         }

    //         var _cities = from c in _list_m select new KeyValuePair<string, int>(c.Key, c.Count());

    //         var _cnae = await _cnaes!.DoListAsync(s => s.Codigo == n);

    //         //var _mongoDB = Factory<VFeatures>.NewDataMongoDB();
    //         var _features = new List<VFeatures>();
    //         var _min_max = new List<int>() { 0 };
    //         foreach (var city in _cities!)
    //         {
    //             var _m = city.Key.ToLower();
    //             var _filter = _m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _m);
    //             foreach (var item in await _geojsonfeatures!.DoListAsync(_filter))
    //             {
    //                 item.Properties!.Empresas = city.Value;
    //                 _min_max.Add(city.Value);
    //                 item.Properties!.Setor = _cnae.SingleOrDefault()?.Descricao;
    //                 _features.Add(item);
    //             }
    //         }

    //         return Ok(new VGeojson
    //         {
    //             Type = "FeatureCollection",
    //             Min = _min_max.OrderByDescending(v => v).Last(),
    //             Max = _min_max.OrderByDescending(v => v).First(),
    //             Features = _features
    //         });
    //     }
    //     catch (Exception ex)
    //     {
    //         return Ok(ex.Message);
    //     }
    // }

    // [HttpGet("geojson/segmento/{n}/{m?}")]
    // public async Task<IActionResult> ListBySegmento([FromRoute] string n, [FromRoute] string? m)
    // {
    //     try
    //     {
    //         var _list = new List<MEmpresa>();

    //         var _n = n[..2];

    //         if (string.IsNullOrEmpty(m))
    //             await foreach (var item in _empresas!.DoListAsync(s => s.CnaeFiscalPrincipal!.StartsWith(_n) && s.SituacaoCadastral == "02", $"segmento_{_n}_{m}"))
    //                 _list.Add(item);

    //         else
    //             await foreach (var item in _empresas!.DoListAsync(s => s.CnaeFiscalPrincipal!.StartsWith(_n) && s.Municipio == m.ToUpper() && s.SituacaoCadastral == "02", $"segmento_{_n}_{m}"))
    //                 _list.Add(item);

    //         var _list_m = _list.GroupBy(s => s.Municipio).OrderByDescending(s => s.Count());

    //         var _cities = from c in _list_m select new KeyValuePair<string, int>(c.Key, c.Count());

    //         var _filter = m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, m);
    //         var _geofeatures = await _geojsonfeatures!.DoListAsync(_filter);

    //         var _features = new List<VFeatures>();
    //         var _min_max = new List<int>() { 0 };
    //         foreach (var city in _cities!)
    //         {
    //             var _m = city.Key.ToLower();
    //             foreach (var item in _geofeatures.Where(s => s.Properties!.Name == _m?.ToLower()))
    //             {
    //                 item.Properties!.Empresas = city.Value;
    //                 _min_max.Add(city.Value);
    //                 item.Properties!.Setor = IDN.Core.Helpers.Dictionaries.CnaesSubClasses[n!];
    //                 _features.Add(item);
    //             }
    //         }

    //         return Ok(new VGeojson
    //         {
    //             Type = "FeatureCollection",
    //             Min = _min_max.OrderByDescending(v => v).Last(),
    //             Max = _min_max.OrderByDescending(v => v).First(),
    //             Features = _features
    //         });
    //     }
    //     catch (Exception ex)
    //     {
    //         return Ok(ex.Message);
    //     }

    // }

    [HttpGet("geojson/segmento/{n}/{m?}")]
    public async Task<IActionResult> ListBySegmentoAsync([FromRoute] string n, [FromRoute] string? m)
    {
        try
        {
            IEnumerable<REmpresas> _list_report;

            var _filter_bson = m == null ?
                                new BsonDocument("TAtividadesDescritivas.v.k", new BsonDocument("$regex", $"^{n}")) :
                                new BsonDocument
                                    {
                                        { "Municipio", m.ToUpper() },
                                        { "TAtividadesDescritivas.v.k", new BsonDocument("$regex", $"^{n}") }
                                    };

            _list_report = await _mongoDB!.DoListAsync(_filter_bson);

            var _cnae = await _cnaes!.DoListAsync(s => s.Codigo!.StartsWith(n));
            var _filter = m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, m);
            var _geofeatures = await _geojsonfeatures!.DoListAsync(_filter);

            var _features = new List<VFeatures>();
            var _min_max = new List<int>() { 0 };
            foreach (var item in _list_report!)
            {
                var _m = item.Municipio!;
                foreach (var f in _geofeatures.Where(s => s.Properties!.Name!.ToLower() == item.Municipio!.ToLower()))
                {
                    string _segmento = string.Empty;
                    int _empresas = 0;
                    foreach (var subitem in item.TAtividadesDescritivas!
                                                .Where(k => k.Key == IDN.Core.Helpers.Dictionaries.SetorProdutivo[n]))
                    {
                        _empresas = subitem.Value
                                                .Where(s => s.Key.StartsWith(n!))
                                                .Sum(s => s.Value);

                        _min_max.Add(_empresas);
                    }

                    f.Properties!.Empresas = _empresas;
                    f.Properties!.Setor = IDN.Core.Helpers.Dictionaries.SetorProdutivo[n!];
                    _features.Add(f);
                }
            }

            return Ok(new VGeojson
            {
                Type = "FeatureCollection",
                Min = 0,
                Max = _min_max.Sum(v => v),
                Features = _features
            });
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet("geojson/cnae/{n}/{m?}")]
    public async Task<IActionResult> ListByCnaeAsync([FromRoute] string n, [FromRoute] string? m)
    {
        try
        {
            IEnumerable<REmpresas> _list_report;

            var _filter_bson = m == null ?
                                new BsonDocument("TAtividadesDescritivas.v.k", new BsonDocument("$regex", $"^{n}")) :
                                new BsonDocument
                                    {
                                        { "Municipio", m.ToUpper() },
                                        { "TAtividadesDescritivas.v.k", new BsonDocument("$regex", $"^{n}") }
                                    };

            _list_report = await _mongoDB!.DoListAsync(_filter_bson);

            var _cnae = await _cnaes!.DoListAsync(s => s.Codigo == n);
            var _filter = m == null ? null : Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, m);
            var _geofeatures = await _geojsonfeatures!.DoListAsync(_filter);

            var _features = new List<VFeatures>();
            var _min_max = new List<int>() { 0 };
            foreach (var item in _list_report!)
            {
                var _m = item.Municipio!;
                foreach (var f in _geofeatures.Where(s => s.Properties!.Name!.ToLower() == item.Municipio!.ToLower()))
                {
                    string _segmento = string.Empty;
                    int _empresas = 0;
                    foreach (var subitem in item.TAtividadesDescritivas!
                                                .Where(k => k.Key == IDN.Core.Helpers.Dictionaries.SetorProdutivo[n![..2]!]))
                    {
                        _empresas = subitem.Value
                                                .Where(s => s.Key.StartsWith(n!))
                                                .Sum(s => s.Value);

                        _min_max.Add(_empresas);
                    }

                    f.Properties!.Empresas = _empresas;
                    f.Properties!.Setor = IDN.Core.Helpers.Dictionaries.SetorProdutivo[n![..2]!];
                    _features.Add(f);
                }
            }

            return Ok(new VGeojson
            {
                Type = "FeatureCollection",
                Min = 0,
                Max = _min_max.Sum(v => v),
                Features = _features
            });
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }


    [HttpGet("geojson/ram-sp/{r?}")]
    public async Task<IActionResult> GetGeocode([FromRoute] string? r)
    {
        try
        {
            var _cities = new List<string>();

            switch (r![..3])
            {
                case "ra-":
                    var _c = r.Remove(0, 3)!.NormalizeText();
                    _cities = Regions.MacroRegoesRASP[_c!].ToList();
                    break;
                case "rg-":
                    var _rg = r.Remove(0, 3)!.NormalizeText();
                    _cities = Regions.MReigoesGovernoSP[_rg!].ToList();
                    break;
                case "rm-":
                    var _rm = r.Remove(0, 3)!.NormalizeText();
                    _cities = Regions.RMetropolitanasSP[_rm!].ToList();
                    break;
                case "au-":
                    var _au = r.Remove(0, 3)!.NormalizeText();
                    _cities = Regions.AUrbanosSP[_au!].ToList();
                    break;
                default:
                    break;
            }

            var _features = new List<VFeatures>();
            var _min_max = new List<int>() { 0 };
            foreach (var city in _cities!)
            {
                var _c = city.NormalizeText().ToLower();
                var __city = Builders<VFeatures>.Filter.Eq(e => e.Properties!.Name, _c);
                foreach (var items in await _geojsonfeatures!.DoListAsync(__city))
                {
                    _min_max.Add(items.Properties!.Empresas!);
                    _features.Add(items);
                }

            };

            return Ok(new VGeojson
            {
                Type = "FeatureCollection",
                Min = 0,
                Max = _min_max.Sum(v => v),
                Features = _features
            });
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}