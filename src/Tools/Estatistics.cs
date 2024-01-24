using System.Data;
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
        //var _dataPG = Factory<MEmpresa>.NewPostgres();
        var _report = new ServiceEmpresa(_serviceEmpresa!, _memoryCache!);
        var _count = 0;

        var _municipios = await _dataDB.ReadDataTableAsync(@"SELECT [Municipio] FROM [dbo].[Empresas] GROUP BY Municipio ORDER BY Municipio");

        foreach (DataRow cidade in _municipios.Rows)
        {
            _count++;
            var _processtimer = new Stopwatch();
            _processtimer.Start();

            _dataDB.ClearParameters();
            _dataDB.AddParameters("@Municipio", cidade[0]);

            var _result = await _report.DoReportEmpresasAsync(_dataDB.ReadStoredProcedureAsync("SELECT * FROM Empresas WHERE Municipio = @Municipio"));

            var _mongoDB2 = Factory<REmpresas>.NewDataMongoDB();
            var _list2 = new List<REmpresas>() { _result };

            await _mongoDB2.InsertManyAsync(_list2);

            _processtimer.Stop();
            Console.WriteLine($"{cidade[0]} : {_list2.Count} : {_processtimer.Elapsed:hh\\:mm\\:ss\\.fff}");
            GC.SuppressFinalize(_result);
        }

        _timer.Stop();
        Console.WriteLine($"Time process : {_timer.Elapsed:hh\\:mm\\:ss\\.fff}");
    }
}