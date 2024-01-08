using System.Linq.Expressions;

namespace IDN.Core;

public interface IRepositoryCore<T> where T : class
{
    Task AddAsync(T obj);
    Task AddRangeAsync(IEnumerable<T> obj);
    Task UpdateAsync(T obj);
    Task RemoveAsync(T obj);
}

