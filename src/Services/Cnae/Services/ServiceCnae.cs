using System.Linq.Expressions;
using AutoMapper;
using IDN.Core.Cnae.Interfaces;
using IDN.Core.Cnae.Models;
using IDN.Services.Cnae.Interfaces;
using IDN.Services.Cnae.View;

namespace IDN.Services.Cnae.Services;

public class ServiceCnae : IServiceCnae
{
    private readonly IServiceCoreCnae _cnae;
    private readonly IMapper _mapper;

    public ServiceCnae(IServiceCoreCnae municipio,
        IMapper mapper)
    {
        _cnae = municipio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VCnae>> DoListAsync(Expression<Func<MCnae, bool>>? param = null)
    {
        var _list = new List<MCnae>();
        await foreach (var item in _cnae.DoListAsync(param))
            _list.Add(item);

        return _mapper.Map<IEnumerable<VCnae>>(_list);
    }
}