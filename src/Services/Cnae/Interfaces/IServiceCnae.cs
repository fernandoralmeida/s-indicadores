using System.Linq.Expressions;
using IDN.Services.Base;
using IDN.Core.Cnae.Models;
using IDN.Services.Cnae.View;

namespace IDN.Services.Cnae.Interfaces;

public interface IServiceCnae : IServiceBase<MCnae>
{
    Task<IEnumerable<VCnae>> DoListAsync(Expression<Func<MCnae, bool>>? param = null);

}