using System.Linq.Expressions;
using IDN.Core.Cnae.Models;

namespace IDN.Core.Cnae.Interfaces;
public interface IServiceCoreCnae : IServiceCore<MCnae>
{
    IAsyncEnumerable<MCnae> DoListAsync(Expression<Func<MCnae, bool>>? param = null);
}

