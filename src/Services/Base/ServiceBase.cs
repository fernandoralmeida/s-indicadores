using IDN.Core;

namespace IDN.Services.Base;

public class ServiceBase<T> : IServiceBase<T> where T : class {
    protected readonly IServiceCore<T> _service;
    public ServiceBase(IServiceCore<T> service)
    {
        _service = service;
    }
}