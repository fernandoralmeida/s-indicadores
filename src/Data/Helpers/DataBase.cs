namespace IDN.Data.Helpers;

public static class DataBase
{
    public static readonly string ConnectionString                
                = Environment.GetEnvironmentVariable("DS_POSTGRES")!;
    public static readonly string DBName = @"www_indicadores";

    public static readonly string MongoDBServer = Environment.GetEnvironmentVariable("DS_MONGODB")!;

    public static readonly string DS_POSTGRES
    = Environment.GetEnvironmentVariable("DS_POSTGRES_VPS")!;
}