using System.Linq.Expressions;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Models;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;
public class RepositoryEmpresa : RepositoryContext<MEmpresa>, IRepositoryCoreEmpresa
{
    // private readonly DataNpgsql _data = new();
    public RepositoryEmpresa(ContextApp data) : base(data) { }

    public IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null)
    {
        var query = _context.Empresas!
                            .Where(param ?? (p => true))
                            .AsNoTrackingWithIdentityResolution();

        return query.AsAsyncEnumerable();
    }

    // public IAsyncEnumerable<MEmpresa> DoStoredProcedure(string sqlquery)
    // {
    //     // var _query = $"SELECT * FROM Empresas WHERE {field} LIKE @param";

    //     // _data.ClearParameters();
    //     // _data.AddParameters("@param", $"{param}%");

    //     // if (!string.IsNullOrEmpty(city))
    //     // {
    //     //     _data.AddParameters("@city", city?.ToUpper()!);
    //     //     _query = $"SELECT * FROM Empresas WHERE Municipio = @city AND {field} LIKE @param";
    //     // }        

    //     return _data.ReadStoredProcedureAsync(sqlquery);
    // }
}