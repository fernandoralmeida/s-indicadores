namespace IDN.Data.Helpers;

public static class DataBase
{
    public static readonly string ConnectionString                
                = Environment.GetEnvironmentVariable("ds_indicadores")!;
    public static readonly string DBName = @"www_indicadores";

    public static readonly string MongoDBServer = @"mongodb://62.72.11.44:27017";// Environment.GetEnvironmentVariable("DS_MONGODB")!;

    public static readonly string DS_POSTGRES
    = Environment.GetEnvironmentVariable("DS_POSTGRES")!;

}