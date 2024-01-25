using System.Data;
using System.Diagnostics;
using Npgsql;

namespace IDN.Tools;

public static class Data
{
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
    {
        using NpgsqlConnection connection = new($"{datasource}Database={database};");
        try
        {
            await connection.OpenAsync();
            NpgsqlCommand _command = connection.CreateCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = query;
            _command.CommandTimeout = 0;

            foreach (NpgsqlParameter p in ParameterCollection.Cast<NpgsqlParameter>())
                _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));

            await _command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static async Task<DataTable> ReadAsync(string query, string database, string datasource)
     => await Task.Run(() =>
        {
            using NpgsqlConnection connection = new($"{datasource}Database={database};");
            try
            {
                connection.Open();

                NpgsqlCommand _command = connection.CreateCommand();
                _command.CommandType = CommandType.Text;
                _command.CommandText = query;
                _command.CommandTimeout = 0;

                foreach (NpgsqlParameter p in ParameterCollection.Cast<NpgsqlParameter>())
                    _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));


                DataTable _table = new();
                new NpgsqlDataAdapter(_command).Fill(_table);

                return _table;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new DataTable();
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
                await writer.WriteAsync(item?.ToString());

        }

        writer.Complete();

        stopwatch.Stop();
        Console.WriteLine($"Inserteds rows: {stopwatch.Elapsed} Process time: {dataTable.Rows.Count}");
    }

    public static async Task CreateDataBase(string sqlscript, string datasource)
    {
        string connectionString = $"{datasource}";
        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sqlscript, conn);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"{sqlscript} successfully created!");                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return;
        }
    }

        public static async Task CreateTables(string sqlscript, string datasource, string database)
    {
        string connectionString = $"{datasource}Database={database};";
        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sqlscript, conn);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"{sqlscript} successfully created!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return;
        }
    }
}