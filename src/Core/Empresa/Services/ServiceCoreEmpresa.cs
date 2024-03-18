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

    // public IAsyncEnumerable<MEmpresa> DoListByCNAE(string param, string? municipio = null)
    // {
    //     return _empresa.DoListByCNAE(param, municipio);
    // }

    // public IAsyncEnumerable<MEmpresa> DoListByMunicipio(string param)
    // {
    //     return _empresa.DoListByMunicipio(param);
    // }

    // public IAsyncEnumerable<MEmpresa> DoListByNJ(string param, string? municipio = null)
    // {
    //     return _empresa.DoListByNJ(param, municipio);
    // }

    // public IAsyncEnumerable<MEmpresa> DoListBySegmento(string param, string? municipio = null)
    // {
    //     return _empresa.DoListBySegmento(param, municipio);
    // }

}