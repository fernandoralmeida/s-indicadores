
namespace IDN.Tools;

public static class DBConfig
{
    public static readonly string MigraData_RFB = @"migradata_rfb";

    public static readonly string IndicadoresNET = @"www_indicadores";

    public static readonly string DS_POSTGRES
        = Environment.GetEnvironmentVariable("DS_POSTGRES")!;

    public static readonly string DS_POSTGRES_VPS
        = Environment.GetEnvironmentVariable("DS_POSTGRES_VPS")!;

    public static readonly string DS_MONGODB
        = Environment.GetEnvironmentVariable("DS_MONGODB")!;

    public static readonly string DS_MONGODB_VPS
        = Environment.GetEnvironmentVariable("DS_MONGODB_VPS")!;

}