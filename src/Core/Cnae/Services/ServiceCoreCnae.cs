using System.Linq.Expressions;
using IDN.Core.Cnae.Interfaces;
using IDN.Core.Cnae.Models;

namespace IDN.Core.Cnae.Services;

public class ServiceCoreCnae : ServiceCore<MCnae>, IServiceCoreCnae
{
    protected readonly IRepositoryCoreCnae _cnae;
    public ServiceCoreCnae(IRepositoryCoreCnae municipio) : base(municipio)
    {
        _cnae = municipio;
    }

    public IAsyncEnumerable<MCnae> DoListAsync(Expression<Func<MCnae, bool>>? param = null)
    {
        return _cnae.DoListAsync(param);
    }

}