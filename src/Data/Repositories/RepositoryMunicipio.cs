using System.Linq.Expressions;
using IDN.Core.Municipio.Interfaces;
using IDN.Core.Municipio.Models;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;

public class RepositoryMunicipio : RepositoryContext<MMunicipio>, IRepositoryCoreMunicipio
{
    
    public RepositoryMunicipio(ContextApp context) : base(context)
    {
        
    }
    public async IAsyncEnumerable<MMunicipio> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null)
    {
        foreach (var item in await _context.Municipios!.ToListAsync())
            yield return item;
    }
}