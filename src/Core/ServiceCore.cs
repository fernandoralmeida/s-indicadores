
namespace IDN.Core;
public class ServiceCore<T> : IServiceCore<T> where T : class
{
    protected readonly IRepositoryCore<T> _repository;

    public ServiceCore(IRepositoryCore<T> repository)
        => _repository = repository;

    public async Task AddAsync(T obj)
    {
        await _repository.AddAsync(obj);
    }

    public async Task AddRangeAsync(IEnumerable<T> obj)
    {
        await _repository.AddRangeAsync(obj);
    }

    public async Task RemoveAsync(T obj)
    {
        await _repository.RemoveAsync(obj);
    }

    public async Task UpdateAsync(T obj)
    {
        await _repository.UpdateAsync(obj);
    }
}

