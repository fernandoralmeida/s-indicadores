using System.Linq.Expressions;
using IDN.Services.Base;
using IDN.Services.Empresa.Records;
using IDN.Core.Empresa.Models;

namespace IDN.Services.Empresa.Interfaces;

public interface IServiceEmpresa : IServiceBase<MEmpresa>
{
    //IAsyncEnumerable<MEmpresa> DoStoredProcedure(string field, string param, string? city = null);
    Task<REmpresas> DoReportEmpresasAsync(IAsyncEnumerable<MEmpresa> lista, Func<MEmpresa, bool>? param = null);
    Task<REmpresas> DoReportEmpresasAsync(IEnumerable<MEmpresa> lista, Func<MEmpresa, bool>? param = null);
    Task<RCharts> DoReportToChartAsync(REmpresas report);
    IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null, string? token = null);
    Task<IEnumerable<string>> DoListMunicipiosEstadoSP();

    Task<IEnumerable<KeyValuePair<int, IEnumerable<(int, int, int)>>>> DoMatrizEmpresarial(IEnumerable<MEmpresa> list);

}