using System.Linq.Expressions;
using AutoMapper;
using IDN.Core.Municipio.Interfaces;
using IDN.Core.Municipio.Models;
using IDN.Services.Municipio.Interfaces;
using IDN.Services.Municipio.View;

namespace IDN.Services.Municipio.Services;

public class ServiceMunicipio : IServiceMunicipio
{
    private readonly IServiceCoreMunicipio _municipio;
    private readonly IMapper _mapper;

    public ServiceMunicipio(IServiceCoreMunicipio municipio,
        IMapper mapper)
    {
        _municipio = municipio;
        _mapper = mapper;
    }

    public IAsyncEnumerable<VMunicipio> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null)
    {
        return _mapper.Map<IAsyncEnumerable<VMunicipio>>(_municipio.DoListAsync(param));
    }

    public async Task<IEnumerable<VMunicipio>> DoMicroRegiaoJauAsync()
    {
        var _list = new List<MMunicipio>();
        await foreach (var item in _municipio.DoListAsync())
            _list.Add(item);

        return _mapper.Map<IEnumerable<VMunicipio>>(_list.Where(s => s.MicroRegiaoJahu(s)));
    }
}