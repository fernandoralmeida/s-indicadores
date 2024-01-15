using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using IDN.Core.Empresa.Interfaces;
using IDN.Services.Empresa.Services;
using Microsoft.Extensions.Caching.Memory;
using Npgsql;

namespace IDN.Tools;

public static class Companies
{
    private static readonly IServiceCoreEmpresa? _serviceEmpresa;
    private static readonly IMemoryCache? _memoryCache;
    private static readonly NpgsqlParameterCollection ParameterCollection = new NpgsqlCommand().Parameters;
    public static void ClearParameters()
    {
        ParameterCollection.Clear();
    }
    public static void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new NpgsqlParameter(parameterName, parameterValue));
    }
    public static async Task WriteAsync(string query, string database, string datasource)
    => await Task.Run(() =>
        {
            using (NpgsqlConnection connection = new($"{datasource}Database={database};"))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = query;
                    _command.CommandTimeout = 0;

                    foreach (NpgsqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                    }

                    var r = _command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        });
    public static async Task<DataTable> ReadAsync(string query, string database, string datasource)
     => await Task.Run(() =>
        {
            using (NpgsqlConnection connection = new($"{datasource}Database={database};"))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = query;
                    _command.CommandTimeout = 0;

                    foreach (NpgsqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                    }

                    DataTable _table = new();

                    new NpgsqlDataAdapter(_command).Fill(_table);

                    return _table;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return new DataTable();
                }
            }
        });
    public static async Task InsertDataBulkCopy(string connectionString, string tableName, DataTable dataTable)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var writer = connection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)");

        foreach (DataRow row in dataTable.Rows)
        {
            // Ajuste os tipos de dados e os valores conforme necessário
            writer.Timeout = TimeSpan.Zero;

            await writer.StartRowAsync();
            foreach (var item in row.ItemArray)
            {
                await writer.WriteAsync(item?.ToString());
            }
        }

        writer.Complete();

        stopwatch.Stop();
        Console.WriteLine($"Process time: {dataTable.Rows.Count} Inserteds rows: {stopwatch.Elapsed}");
    }
    public static async Task CreateMigraData_RFB(string database, string datasource)
    {
        string connectionString = $"{datasource};Database=postgres;";
        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using var cmd = new NpgsqlCommand($"CREATE DATABASE {database}", conn);
                cmd.ExecuteScalar();
                Console.WriteLine($"{database} successfully created!");
            }

            try
            {
                await WriteAsync(SqlScript.MigraData_RFB, database, datasource);
                Console.WriteLine($"Tables successfully created!");
                await WriteAsync(SqlScript.Create_view_postgres_migradata_rfb, database, datasource);
                Console.WriteLine($"View successfully created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}:");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    public static async Task DoCNAE(string database, string datasource)
    {
        int i = 0;
        var _insert = SqlCommands.InsertCommand("Cnaes", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".CNAECSV"))
            try
            {
                Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                //var _data = Factory.Data(server);

                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        ClearParameters();
                        AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await WriteAsync(_insert, database, datasource);
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }
    public static async Task DoMotivo(string database, string datasource)
        => await Task.Run(async () =>
        {
            int i = 0;
            var _insert = SqlCommands.InsertCommand("MotivoSituacaoCadastral", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".MOTICSV"))
                try
                {
                    Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            ClearParameters();
                            AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                            AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                            await WriteAsync(_insert, database, datasource);
                            i++;
                        }

                    _timer.Stop();
                    Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
        });
    public static async Task DoMunicipios(string database, string datasource)
    {
        int i = 0;

        var _insert = SqlCommands.InsertCommand("Municipios", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".MUNICCSV"))
            try
            {
                Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        ClearParameters();
                        AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await WriteAsync(_insert, database, datasource);
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }
    public static async Task DoNaturezaJuridica(string database, string datasource)
    {
        int i = 0;
        var _insert = SqlCommands.InsertCommand("NaturezaJuridica", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);
        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".NATJUCSV"))
            try
            {
                Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        ClearParameters();
                        AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await WriteAsync(_insert, database, datasource);
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }
    public static async Task DoPaises(string database, string datasource)
    {
        int i = 0;

        var _insert = SqlCommands.InsertCommand("Paises", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".PAISCSV"))
            try
            {
                Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        ClearParameters();
                        AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await WriteAsync(_insert, database, datasource);
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }
    public static async Task DoQualificacaoSocios(string database, string datasource)
    {
        int i = 0;

        var _insert = SqlCommands.InsertCommand("QualificacaoSocios", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".QUALSCSV"))
            try
            {
                Console.WriteLine($"Migrating File {Path.GetFileName(file)}");

                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        ClearParameters();
                        AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await WriteAsync(_insert, database, datasource);
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }

    public static async Task DoEstabelecimentos(string database, string datasource)
    {
        int c1;
        int c2;
        int tc1 = 0;
        int tc2 = 0;

        var connectionString = $"{datasource}Database={database};";
        var tableName = "Estabelecimentos";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".ESTABELE"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("CNPJOrdem");
                dataTable.Columns.Add("CNPJDV");
                dataTable.Columns.Add("IdentificadorMatrizFilial");
                dataTable.Columns.Add("NomeFantasia");
                dataTable.Columns.Add("SituacaoCadastral");
                dataTable.Columns.Add("DataSituacaoCadastral");
                dataTable.Columns.Add("MotivoSituacaoCadastral");
                dataTable.Columns.Add("NomeCidadeExterior");
                dataTable.Columns.Add("Pais");
                dataTable.Columns.Add("DataInicioAtividade");
                dataTable.Columns.Add("CnaeFiscalPrincipal");
                dataTable.Columns.Add("CnaeFiscalSecundaria");
                dataTable.Columns.Add("TipoLogradouro");
                dataTable.Columns.Add("Logradouro");
                dataTable.Columns.Add("Numero");
                dataTable.Columns.Add("Complemento");
                dataTable.Columns.Add("Bairro");
                dataTable.Columns.Add("CEP");
                dataTable.Columns.Add("UF");
                dataTable.Columns.Add("Municipio");
                dataTable.Columns.Add("DDD1");
                dataTable.Columns.Add("Telefone1");
                dataTable.Columns.Add("DDD2");
                dataTable.Columns.Add("Telefone2");
                dataTable.Columns.Add("DDDFax");
                dataTable.Columns.Add("Fax");
                dataTable.Columns.Add("CorreioEletronico");
                dataTable.Columns.Add("SituacaoEspecial");
                dataTable.Columns.Add("DataSitucaoEspecial");

                c2 = 0;
                c1 = 0;

                Console.WriteLine($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');

                        var _uf = fields[19].ToString().Replace("\"", "").Trim();
                        var _cidade = fields[20].ToString().Replace("\"", "").Trim();
                        c1++;

                        if (_uf == "SP")
                        {
                            // Adicionar a linha à DataTable
                            DataRow row = dataTable.NewRow();
                            row["CNPJBase"] = fields[0].ToString().Replace("\"", "").Trim();
                            row["CNPJOrdem"] = fields[1].ToString().Replace("\"", "").Trim();
                            row["CNPJDV"] = fields[2].ToString().Replace("\"", "").Trim();
                            row["IdentificadorMatrizFilial"] = fields[3].ToString().Replace("\"", "").Trim();
                            row["NomeFantasia"] = fields[4].ToString().Replace("\"", "").Trim();
                            row["SituacaoCadastral"] = fields[5].ToString().Replace("\"", "").Trim();
                            row["DataSituacaoCadastral"] = fields[6].ToString().Replace("\"", "").Trim();
                            row["MotivoSituacaoCadastral"] = fields[7].ToString().Replace("\"", "").Trim();
                            row["NomeCidadeExterior"] = fields[8].ToString().Replace("\"", "").Trim();
                            row["Pais"] = fields[9].ToString().Replace("\"", "").Trim();
                            row["DataInicioAtividade"] = fields[10].ToString().Replace("\"", "").Trim();
                            row["CnaeFiscalPrincipal"] = fields[11].ToString().Replace("\"", "").Trim();
                            row["CnaeFiscalSecundaria"] = fields[12].ToString().Replace("\"", "").Trim();
                            row["TipoLogradouro"] = fields[13].ToString().Replace("\"", "").Trim();
                            row["Logradouro"] = fields[14].ToString().Replace("\"", "").Trim();
                            row["Numero"] = fields[15].ToString().Replace("\"", "").Trim();
                            row["Complemento"] = fields[16].ToString().Replace("\"", "").Trim();
                            row["Bairro"] = fields[17].ToString().Replace("\"", "").Trim();
                            row["CEP"] = fields[18].ToString().Replace("\"", "").Trim();
                            row["UF"] = fields[19].ToString().Replace("\"", "").Trim();
                            row["Municipio"] = fields[20].ToString().Replace("\"", "").Trim();
                            row["DDD1"] = fields[21].ToString().Replace("\"", "").Trim();
                            row["Telefone1"] = fields[22].ToString().Replace("\"", "").Trim();
                            row["DDD2"] = fields[23].ToString().Replace("\"", "").Trim();
                            row["Telefone2"] = fields[24].ToString().Replace("\"", "").Trim();
                            row["DDDFax"] = fields[25].ToString().Replace("\"", "").Trim();
                            row["Fax"] = fields[26].ToString().Replace("\"", "").Trim();
                            row["CorreioEletronico"] = fields[27].ToString().Replace("\"", "").Trim();
                            row["SituacaoEspecial"] = fields[28].ToString().Replace("\"", "").Trim();
                            row["DataSitucaoEspecial"] = fields[29].ToString().Replace("\"", "").Trim();

                            dataTable.Rows.Add(row);

                            c2++;

                            if (c2 % 100 == 0)
                            {
                                Console.Write($"  {c2}");
                                Console.Write("\r");
                            }
                        }
                    }

                    // Usar SqlBulkCopy para inserir os dados na tabela do banco de dados
                    await InsertDataBulkCopy(connectionString, tableName, dataTable);
                }
                tc1 += c1;
                tc2 += c2;
                dataTable.Dispose();
            }
            _timer.Stop();
            Console.WriteLine($"TLines: {tc1} | TMigrated: {tc2} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
    public static async Task DoEmpresas(string database, string datasource)
    {
        int _tcount = 0;

        var connectionString = $"{datasource}Database={database};";
        var tableName = "Empresas";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".EMPRECSV"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("RazaoSocial");
                dataTable.Columns.Add("NaturezaJuridica");
                dataTable.Columns.Add("QualificacaoResponsavel");
                dataTable.Columns.Add("CapitalSocial");
                dataTable.Columns.Add("PorteEmpresa");
                dataTable.Columns.Add("EnteFederativoResponsavel");

                int _count = 0;

                Console.WriteLine($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');

                        _count++;

                        DataRow row = dataTable.NewRow();
                        row["CNPJBase"] = fields[0].ToString().Replace("\"", "").Trim();
                        row["RazaoSocial"] = fields[1].ToString().Replace("\"", "").Trim();
                        row["NaturezaJuridica"] = fields[2].ToString().Replace("\"", "").Length <= 4 ? fields[2].ToString().Replace("\"", "").Trim() : "0000";
                        row["QualificacaoResponsavel"] = fields[3].ToString().Replace("\"", "").Length <= 2 ? fields[3].ToString().Replace("\"", "").Trim() : "00";
                        row["CapitalSocial"] = fields[4].ToString().Replace("\"", "").Trim()!;
                        row["PorteEmpresa"] = fields[5].ToString().Replace("\"", "").Length <= 2 ? fields[5].ToString().Replace("\"", "").Trim() : "00";
                        row["EnteFederativoResponsavel"] = fields[6].ToString().Replace("\"", "").Trim();

                        dataTable.Rows.Add(row);

                        if (_count % 100 == 0)
                        {
                            Console.Write($"  {_count}");
                            Console.Write("\r");
                        }
                    }

                    await InsertDataBulkCopy(connectionString, tableName, dataTable);
                }

                _tcount += _count;
                dataTable.Dispose();
            }
            _timer.Stop();
            Console.WriteLine($"TLines: {_tcount} | TMigrated: {_tcount} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
    public static async Task DoSimples(string database, string datasource)
    {
        int _count = 0;
        var connectionString = $"{datasource}Database={database};";
        var tableName = "Simples";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".D3"))
            {
                _count = 0;

                Console.WriteLine($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                var _lista_simples_completa = new List<string>();

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        _count++;
                        _lista_simples_completa.Add(line!);

                        if (_count % 10000 == 0)
                        {
                            Console.Write($"  {_count}");
                            Console.Write("\r");
                        }
                    }

                    int _parts = 10;
                    int _size_parts = _lista_simples_completa.Count / _parts;

                    //quebra em 10 partes iguais                    
                    //para cada grupo, execute o codigo;
                    foreach (var parts in Enumerable
                                            .Range(0, _parts)
                                            .Select(s => _lista_simples_completa.Skip(s * _size_parts).Take(_size_parts))
                                            .ToList())
                    {
                        var _rows = 0;
                        Console.Write("\n|");

                        var dataTable = new DataTable();
                        dataTable.Columns.Add("CNPJBase");
                        dataTable.Columns.Add("OpcaoSimples");
                        dataTable.Columns.Add("DataOpcaoSimples");
                        dataTable.Columns.Add("DataExclusaoSimples");
                        dataTable.Columns.Add("OpcaoMEI");
                        dataTable.Columns.Add("DataOpcaoMEI");
                        dataTable.Columns.Add("DataExclusaoMEI");

                        foreach (var item in parts)
                        {
                            var fields = item!.Split(';');
                            _rows++;
                            // Adicionar a linha à DataTable                            
                            DataRow row = dataTable.NewRow();
                            row["CNPJBase"] = item[0].ToString().Replace("\"", "").Trim();
                            row["OpcaoSimples"] = item[1].ToString().Replace("\"", "").Trim();
                            row["DataOpcaoSimples"] = item[2].ToString().Replace("\"", "").Trim();
                            row["DataExclusaoSimples"] = item[3].ToString().Replace("\"", "").Trim();
                            row["OpcaoMEI"] = item[4].ToString().Replace("\"", "").Trim();
                            row["DataOpcaoMEI"] = item[5].ToString().Replace("\"", "").Trim();
                            row["DataExclusaoMEI"] = item[6].ToString().Replace("\"", "").Trim();
                            dataTable.Rows.Add(row);

                            if (_rows % 100 == 0)
                            {
                                Console.Write($"  {_rows}");
                                Console.Write("\r");
                            }
                        }

                        // Usar SqlBulkCopy para inserir os dados na tabela do banco de dados
                        await InsertDataBulkCopy(connectionString, tableName, dataTable);
                        dataTable.Dispose();
                    }
                }

            }
            _timer.Stop();
            Console.WriteLine($"TLines: {_count} | TMigrated: {_count} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
    public static async Task DoSocios(string database, string datasource)
    {
        int _count = 0;
        int _tcount = 0;
        var connectionString = $"{datasource}Database={database};";
        var tableName = "Socios";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".SOCIOCSV"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("IdentificadorSocio");
                dataTable.Columns.Add("NomeRazaoSocio");
                dataTable.Columns.Add("CnpjCpfSocio");
                dataTable.Columns.Add("QualificacaoSocio");
                dataTable.Columns.Add("DataEntradaSociedade");
                dataTable.Columns.Add("Pais");
                dataTable.Columns.Add("RepresentanteLegal");
                dataTable.Columns.Add("NomeRepresentante");
                dataTable.Columns.Add("QualificacaoRepresentanteLegal");
                dataTable.Columns.Add("FaixaEtaria");

                _count = 0;

                Console.WriteLine($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');

                        _count++;

                        // Adicionar a linha à DataTable
                        DataRow row = dataTable.NewRow();
                        row["CNPJBase"] = fields[0].ToString().Replace("\"", "").Trim();
                        row["IdentificadorSocio"] = fields[1].ToString().Replace("\"", "").Trim();
                        row["NomeRazaoSocio"] = fields[2].ToString().Replace("\"", "").Trim();
                        row["CnpjCpfSocio"] = fields[3].ToString().Replace("\"", "").Trim();
                        row["QualificacaoSocio"] = fields[4].ToString().Replace("\"", "").Trim();
                        row["DataEntradaSociedade"] = fields[5].ToString().Replace("\"", "").Trim();
                        row["Pais"] = fields[6].ToString().Replace("\"", "").Trim();
                        row["RepresentanteLegal"] = fields[7].ToString().Replace("\"", "").Trim();
                        row["NomeRepresentante"] = fields[8].ToString().Replace("\"", "").Trim();
                        row["QualificacaoRepresentanteLegal"] = fields[9].ToString().Replace("\"", "").Trim();
                        row["FaixaEtaria"] = fields[10].ToString().Replace("\"", "").Trim();

                        dataTable.Rows.Add(row);

                        if (_count % 100 == 0)
                        {
                            Console.Write($"  {_count}");
                            Console.Write("\r");
                        }

                    }

                    // Usar SqlBulkCopy para inserir os dados na tabela do banco de dados
                    await InsertDataBulkCopy(connectionString, tableName, dataTable);

                }
                _tcount += _count;
                dataTable.Dispose();
            }
            _timer.Stop();
            Console.WriteLine($"TLines: {_tcount} | TMigrated: {_tcount} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    public static async Task CreateIndicadoresNet(string database, string datasource)
    {
        string connectionString = $"{datasource}Database={database};";
        try
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Console.WriteLine($"{database} OK!");
        }
        catch
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"CREATE DATABASE {database}", conn))
                    {
                        cmd.ExecuteScalar();
                        Console.WriteLine($"{database} successfully created!");
                    }
                }

                await WriteAsync(SqlScript.MigraData_RFB, database, datasource);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: Database OK!");
            }
        }
    }

    public static async Task DoIndicadores(string databaseOut, string databaseIn, string datasource)
    {
        var _cidades = new ServiceEmpresa(_serviceEmpresa!, _memoryCache!);
        var tableName = "Empresas";

        var _timer = new Stopwatch();
        _timer.Start();

        var _count = 0;
        var _trows = 0;

        Console.WriteLine($"Starting Migrate to Indicadores");

        try
        {
            foreach (var cidade in await _cidades.DoListMunicipiosEstadoSP())
            {
                _count++;
                var _processtimer = new Stopwatch();
                _processtimer.Start();
                ClearParameters();
                AddParameters("@Municipio", cidade);
                var _dtable = await ReadAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource);
                _trows += _dtable.Rows.Count;

                Console.WriteLine($"M: {cidade} | Rows: {_dtable.Rows.Count}");

                await InsertDataBulkCopy(databaseIn, tableName, _dtable);

                Console.WriteLine($"M: {cidade} | Rows Migrated: {_dtable.Rows.Count} | Time: {_processtimer.Elapsed:hh\\:mm\\:ss}");
                _processtimer.Start();
            }

            _timer.Stop();
            Console.WriteLine($"Cities: {_count} | Rows: {_trows} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}