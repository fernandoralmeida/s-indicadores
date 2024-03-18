using System.Linq.Expressions;
using IDN.Core.Empresa.Models;

namespace IDN.Core.Empresa.Interfaces;

public interface IRepositoryCoreEmpresa : IRepositoryCore<MEmpresa>
{
    IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null);
    // IAsyncEnumerable<MEmpresa> DoListByMunicipio(string param);
    // IAsyncEnumerable<MEmpresa> DoListByCNAE(string param, string? municipio = null);
    // IAsyncEnumerable<MEmpresa> DoListBySegmento(string param, string? municipio = null);
    // IAsyncEnumerable<MEmpresa> DoListByNJ(string param, string? municipio = null);
}

