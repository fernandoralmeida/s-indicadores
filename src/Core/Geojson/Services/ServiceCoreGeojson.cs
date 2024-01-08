using System.Linq.Expressions;
using IDN.Core.Geojson.Interfaces;
using IDN.Core.Geojson.Models;

namespace IDN.Core.Empresa.Services;

public class ServiceCoreGeojson : ServiceCore<MFeatures>, IServiceCoreGeojson
{
    private readonly IRepositoryCoreGeojson _data;
    public ServiceCoreGeojson(IRepositoryCoreGeojson data) : base(data)
    {
        _data = data;
    }
    public Task<MGeojson> DoGeojsonAsync(Expression<Func<MFeatures, bool>>? filter = null)
    {
        return _data.DoGeojsonAsync(filter);
    }
}