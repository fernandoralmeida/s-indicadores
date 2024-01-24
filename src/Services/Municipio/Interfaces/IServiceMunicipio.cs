using System.Linq.Expressions;
using IDN.Services.Base;
using IDN.Core.Municipio.Models;
using IDN.Services.Municipio.View;

namespace IDN.Services.Municipio.Interfaces;

public interface IServiceMunicipio : IServiceBase<MMunicipio>
{
    Task<IEnumerable<VMunicipio>> DoListAsync(Expression<Func<MMunicipio, bool>>? param = null);

    Task<IEnumerable<VMunicipio>> DoMicroRegiaoJauAsync();
}