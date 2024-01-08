using System.Linq.Expressions;
using IDN.Core.Municipio.Interfaces;
using IDN.Core.Municipio.Models;

namespace IDN.Core.Municipio.Services;

public class ServiceCoreMunicipio : ServiceCore<MMunicipio>, IServiceCoreMunicipio
{
    protected readonly IRepositoryCoreMunicipio _municipio;
    public ServiceCoreMunicipio(IRepositoryCoreMunicipio municipio) : base(municipio)
    {
        _municipio = municipio;
    }

    public IAsyncEnumerable<MMunicipio> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null)
    {
        return _municipio.DoListAsync(param);
    }

}