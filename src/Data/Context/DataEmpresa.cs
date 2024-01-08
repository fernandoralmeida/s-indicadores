using IDN.Data.Interface;
using Microsoft.Data.SqlClient;
using IDN.Core.Empresa.Models;
using IDN.Data.Helpers;

namespace IDN.Data.Context;

public class DataEmpresa : IData<MEmpresa>
{
    private readonly SqlParameterCollection ParameterCollection = new SqlCommand().Parameters;

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new SqlParameter(parameterName, parameterValue));
    }

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public async IAsyncEnumerable<MEmpresa> ReadStoredProcedureAsync(string query)
    {
        using (SqlConnection connection = new(DataBase.ConnectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            foreach (SqlParameter p in ParameterCollection)
                command.Parameters.AddWithValue(p.ParameterName, p.Value);
            
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {                    
                    yield return new MEmpresa()
                    {
                        CNPJ = reader["CNPJ"] == null ? reader["CNPJ"].ToString() : "",
                        RazaoSocial = reader["RazaoSocial"].ToString(),
                        NaturezaJuridica = reader["NaturezaJuridica"].ToString(),
                        CapitalSocial = Convert.ToDecimal(reader["CapitalSocial"]),
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
}

