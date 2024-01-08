using System.Linq.Expressions;
using IDN.Services.Base;
using IDN.Services.Empresa.Records;
using IDN.Core.Empresa.Models;

namespace IDN.Services.Empresa.Interfaces;

public interface IServiceEmpresa : IServiceBase<MEmpresa>
{
    IAsyncEnumerable<MEmpresa> DoStoredProcedure(string param);
    Task<REmpresas> DoReportEmpresasAsync(IAsyncEnumerable<MEmpresa> lista, Func<MEmpresa, bool>? param = null);
    Task<RCharts> DoReportToChartAsync(REmpresas report);
    IAsyncEnumerable<MEmpresa> DoListAsync(string? municipio = null);
    Task<IEnumerable<string>> DoListMunicipiosEstadoSP();
}