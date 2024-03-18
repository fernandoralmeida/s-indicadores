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
    public IAsyncEnumerable<MMunicipio> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null)
    {
        var query = _context.Municipios!
                            .Where(param ?? (p => true))
                            .AsNoTrackingWithIdentityResolution();

        return query.AsAsyncEnumerable();
    }
}