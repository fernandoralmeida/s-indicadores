using IDN.Core;
using IDN.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Repositories;

public class RepositoryContext<T> : IRepositoryCore<T> where T : class
{
    protected readonly ContextApp _context;

    public RepositoryContext(ContextApp context)
    {
        _context = context;
    }

    public async Task AddAsync(T obj)
    {
        await _context.Set<T>().AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> obj)
    {
        await _context.Set<T>().AddRangeAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(T obj)
    {
        await Task.Run(() => _context.Set<T>().Remove(obj));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T obj)
    {
        await Task.Run(() => _context.Entry(obj).State = EntityState.Modified);
        await _context.SaveChangesAsync();
    }
}