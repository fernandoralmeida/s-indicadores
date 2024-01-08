using System.Linq.Expressions;
using IDN.Core.Municipio.Models;

namespace IDN.Core.Municipio.Interfaces;

public interface IRepositoryCoreMunicipio : IRepositoryCore<MMunicipio>
{
    IAsyncEnumerable<MMunicipio> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null);
}