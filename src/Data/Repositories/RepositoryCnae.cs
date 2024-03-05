using System.Linq.Expressions;
using IDN.Core.Cnae.Interfaces;
using IDN.Core.Cnae.Models;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;

public class RepositoryCnae : RepositoryContext<MCnae>, IRepositoryCoreCnae
{

    public RepositoryCnae(ContextApp context) : base(context)
    {

    }
    public async IAsyncEnumerable<MCnae> DoListAsync(Expression<Func<MCnae, bool>>? param = null)
    {
        foreach (var item in await _context.Cnaes!
                            .Where(param ?? (p => true))
                            .AsNoTrackingWithIdentityResolution()
                            .ToListAsync())
            yield return item;
    }
}