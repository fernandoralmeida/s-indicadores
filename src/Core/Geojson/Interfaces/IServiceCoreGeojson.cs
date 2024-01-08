using System.Linq.Expressions;
using IDN.Core.Geojson.Models;

namespace IDN.Core.Geojson.Interfaces;

public interface IServiceCoreGeojson : IServiceCore<MFeatures>
{
    Task<MGeojson> DoGeojsonAsync(Expression<Func<MFeatures, bool>>? filter = null);
}