using MongoDB.Driver;

namespace IDN.Data.Interface;

public interface IMongoDB<T> where T : class
{
    Task<int> InsertManyAsync(IEnumerable<T> list);
    Task<IEnumerable<T>> DoListAsync(FilterDefinition<T>? filter = null);
}