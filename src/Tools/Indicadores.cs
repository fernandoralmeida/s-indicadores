using System.Diagnostics;

namespace IDN.Tools;

public static class Indicadores
{
    public static async Task DoIndicadores(string ds_migradata, string migradata_db, string ds_indicadores, string indicadores_db)
    {

        var tableName = "Empresas";

        var _timer = new Stopwatch();
        _timer.Start();

        Console.WriteLine($"Populate Indicadores");
        try
        {
            var _count = 0;
            var _trows = 0;
            var _m_count = 0;

            char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            foreach (var _char in alfabeto)
            {
                _count++;
                var _processtimer = new Stopwatch();
                _processtimer.Start();

                Data.ClearParameters();
                Console.WriteLine($"Reading Municipios, inicial: {_char}...");
                var _municipios = await Data.ReadAsync($"SELECT municipio FROM public.view_municipios WHERE municipio LIKE '{_char}%' GROUP BY municipio;", migradata_db, ds_migradata);
                _m_count += _municipios.Rows.Count;
                //var _dtable = await Data.ReadAsync($"SELECT * FROM view_empresas_by_municipio WHERE municipio LIKE '{_char}%' ORDER BY municipio;", migradata_db, ds_migradata);
                var _dtable = await Data.ReadAsync($"SELECT * FROM view_empresas_by_municipio WHERE municipio LIKE '{_char}%' AND situacaocadastral = '02' ORDER BY municipio;", migradata_db, ds_migradata); //VPS

                _trows += _dtable.Rows.Count;

                Console.WriteLine($"Insert rows...");
                await Data.InsertDataBulkCopy($"{ds_indicadores}Database={indicadores_db};", tableName, _dtable);
                
                _processtimer.Stop();
                Console.WriteLine($"Municipios: {_municipios.Rows.Count} | Rows: {_dtable.Rows.Count} | Time: {_processtimer.Elapsed:hh\\:mm\\:ss\\.fff}");
            }

            _timer.Stop();
            Console.WriteLine($"Municipios: {_m_count} Rows: {_trows} | Time: {_timer.Elapsed:hh\\:mm\\:ss\\.fff}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}