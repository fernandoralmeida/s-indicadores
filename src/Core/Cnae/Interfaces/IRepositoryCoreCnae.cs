using System.Linq.Expressions;
using IDN.Core.Cnae.Models;

namespace IDN.Core.Cnae.Interfaces;

public interface IRepositoryCoreCnae : IRepositoryCore<MCnae>
{
    IAsyncEnumerable<MCnae> DoListAsync(Expression<Func<MCnae, bool>>? param = null);
}