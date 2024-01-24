using System.Linq.Expressions;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Models;

namespace IDN.Core.Empresa.Services;

public class ServiceCoreEmpresa : ServiceCore<MEmpresa>, IServiceCoreEmpresa
{
    protected readonly IRepositoryCoreEmpresa _empresa;
    public ServiceCoreEmpresa(IRepositoryCoreEmpresa empresa) : base(empresa)
    {
        _empresa = empresa;
    }

    public IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null)
    {
        return _empresa.DoListAsync(param);
    }

    public IAsyncEnumerable<MEmpresa> DoStoredProcedure(string paran)
    {
        return _empresa.DoStoredProcedure(paran);
    }

    
}