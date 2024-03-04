using System.Data;

namespace IDN.Data.Interface;

public interface IData<T> where T : class
{
    void ClearParameters();
    void AddParameters(string parameterName, object parameterValue);
    IAsyncEnumerable<T> ReadStoredProcedureAsync(string query);
    IAsyncEnumerable<T> ReadSqlQueryAsync(string query);
    Task<DataTable> ReadDataTableAsync(string query);


}