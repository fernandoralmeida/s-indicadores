using System.Linq.Expressions;
using IDN.Core.Geojson.Models;

namespace IDN.Core.Geojson.Interfaces;

public interface IRepositoryCoreGeojson : IRepositoryCore<MFeatures>
{
    Task<MGeojson> DoGeojsonAsync(Expression<Func<MFeatures, bool>>? filter = null);
}