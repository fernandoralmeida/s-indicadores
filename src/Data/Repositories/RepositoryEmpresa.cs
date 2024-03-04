using System.Linq.Expressions;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Models;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;
public class RepositoryEmpresa : RepositoryContext<MEmpresa>, IRepositoryCoreEmpresa
{
    private readonly DataNpgsql _data = new();
    public RepositoryEmpresa(ContextApp data) : base(data) { }

    public async IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null)
    {
        var _query = _context.Empresas!.AsQueryable();

        if (param != null)
            _query = _query.Where(param).AsNoTrackingWithIdentityResolution();

        foreach (var item in await _query.ToListAsync())
            yield return item;
    }

    public IAsyncEnumerable<MEmpresa> DoStoredProcedure(string field, string param, string? city = null)
    {
        var _query = $"SELECT * FROM Empresas WHERE {field} = @param";

        _data.ClearParameters();
        _data.AddParameters("@param", param);

        if (!string.IsNullOrEmpty(city))
        {
            _data.AddParameters("@city", city?.ToUpper()!);
            _query = $"SELECT * FROM Empresas WHERE Municipio = @city AND {field} = @param";
        }        

        return _data.ReadStoredProcedureAsync(_query);
    }
}