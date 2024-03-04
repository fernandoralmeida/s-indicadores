using System.Linq.Expressions;
using IDN.Core.Empresa.Models;

namespace IDN.Core.Empresa.Interfaces;
public interface IServiceCoreEmpresa : IServiceCore<MEmpresa>
{
    IAsyncEnumerable<MEmpresa> DoStoredProcedure(string field, string param, string? city = null);
    IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null);
}

