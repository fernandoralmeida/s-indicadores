using System.Linq.Expressions;
using IDN.Core.Geojson.Interfaces;
using IDN.Core.Geojson.Models;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;

public class RepositoryGeojson : RepositoryContext<MFeatures>, IRepositoryCoreGeojson
{
    public RepositoryGeojson(ContextApp context) : base(context)
    { }
    public async Task<MGeojson> DoGeojsonAsync(Expression<Func<MFeatures, bool>>? param = null)
    {
        var _query = _context.GeojsonFeatures!.AsQueryable();

        if (param != null)
            _query = _query
                .Where(param)
                .Include(s => s.Geometry).ThenInclude(i => i!.Coordinates)
                .Include(s => s.Properties)
                .AsNoTrackingWithIdentityResolution();

        return new MGeojson()
        {
            Type = "FeatureCollection",
            Features = await _query.ToListAsync()
        };
    }
}