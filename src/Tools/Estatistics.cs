using System.Diagnostics;
using System.Text;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Models;
using IDN.Data.Helpers;
using IDN.Services.Empresa.Records;
using IDN.Services.Empresa.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IDN.Tools;

public static class Estatistics
{
    private static readonly IServiceCoreEmpresa? _serviceEmpresa;
    private static readonly IMemoryCache? _memoryCache;
    public static async Task Update()
    {
        Stopwatch _timer = new();
        _timer.Start();
        var _mongoDB = Factory<MEmpresa>.NewDataMongoDB();
        var _dataDB = Factory<MEmpresa>.NewDataEmpresa();
        var _report = new ServiceEmpresa(_serviceEmpresa!, _memoryCache!);
        var _count = 0;

        foreach (var cidade in await _report.DoListMunicipiosEstadoSP())
        {
            _count++;
            var _processtimer = new Stopwatch();
            _processtimer.Start();

            _dataDB.ClearParameters();
            _dataDB.AddParameters("@municipio", cidade);

            var _result = await _report.DoReportEmpresasAsync(_dataDB.ReadStoredProcedureAsync("SELECT * FROM Empresas WHERE Municipio = @municipio"));

            var _mongoDB2 = Factory<REmpresas>.NewDataMongoDB();
            var _list2 = new List<REmpresas>() { _result };

            await _mongoDB2.InsertManyAsync(_list2);

            _processtimer.Stop();
            Console.WriteLine($"{cidade} : {_list2.Count} : {_processtimer.Elapsed:hh\\:mm\\:ss\\.fff}");
            GC.SuppressFinalize(_result);
        }

        _timer.Stop();
        Console.WriteLine($"Time process : {_timer.Elapsed:hh\\:mm\\:ss\\.fff}");
    }
}