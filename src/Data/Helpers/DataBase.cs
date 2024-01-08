namespace IDN.Data.Helpers;

public static class DataBase
{
    public static readonly string ConnectionString                
                = Environment.GetEnvironmentVariable("ds_indicadores")!;
    public static readonly string DBName = @"www_indicadores";

}