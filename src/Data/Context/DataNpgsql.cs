using IDN.Data.Interface;
using IDN.Core.Empresa.Models;
using IDN.Data.Helpers;
using Npgsql;
using System.Data;

namespace IDN.Data.Context;

public class DataNpgsql : IData<MEmpresa>
{
    private readonly NpgsqlParameterCollection ParameterCollection = new NpgsqlCommand().Parameters;

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new NpgsqlParameter(parameterName, parameterValue));
    }

    public async IAsyncEnumerable<MEmpresa> ReadStoredProcedureAsync(string query)
    {
        using (NpgsqlConnection connection = new(DataBase.DS_POSTGRES_VPS + $"Database={Helpers.DataBase.DBName};"))
        {
            await connection.OpenAsync();

            var command = new NpgsqlCommand(query, connection)
            {
                CommandTimeout = 0
            };

            foreach (NpgsqlParameter p in ParameterCollection.Cast<NpgsqlParameter>())
                command.Parameters.AddWithValue(p.ParameterName, p.Value!);
            
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {                    
                    yield return new MEmpresa()
                    {
                        CNPJ = reader["CNPJ"] == null ? reader["CNPJ"].ToString() : "",
                        RazaoSocial = reader["RazaoSocial"].ToString(),
                        NaturezaJuridica = reader["NaturezaJuridica"].ToString(),
                        CapitalSocial = reader["CapitalSocial"].ToString() == "LTDA" ? 0 : Convert.ToDecimal(reader["CapitalSocial"]),
                        PorteEmpresa = reader["PorteEmpresa"].ToString(),
                        IdentificadorMatrizFilial = reader["IdentificadorMatrizFilial"].ToString(),
                        NomeFantasia = reader["NomeFantasia"].ToString(),
                        SituacaoCadastral = reader["SituacaoCadastral"].ToString(),
                        DataSituacaoCadastral = reader["DataSituacaoCadastral"].ToString(),
                        DataInicioAtividade = reader["DataInicioAtividade"].ToString(),
                        CnaeFiscalPrincipal = reader["CnaeFiscalPrincipal"].ToString(),
                        CnaeDescricao = reader["CnaeDescricao"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        Logradouro = reader["Logradouro"].ToString(),
                        Numero = reader["Numero"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        UF = reader["UF"].ToString(),
                        Municipio = reader["Municipio"].ToString(),
                        OpcaoSimples = reader["OpcaoSimples"].ToString(),
                        DataOpcaoSimples = reader["DataOpcaoSimples"].ToString(),
                        DataExclusaoSimples = reader["DataExclusaoSimples"].ToString(),
                        OpcaoMEI = reader["OpcaoMEI"].ToString(),
                        DataOpcaoMEI = reader["DataOpcaoMEI"].ToString(),
                        DataExclusaoMEI = reader["DataExclusaoMEI"].ToString()
                    };

                    //yield return Mapper.Map<MEmpresa>(reader);
                }
            }
        }
    }

    public async Task<DataTable> ReadDataTableAsync(string query)
     => await Task.Run(() =>
        {
            using (NpgsqlConnection connection = new(DataBase.DS_POSTGRES_VPS + $"Database={Helpers.DataBase.DBName};"))
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
}

