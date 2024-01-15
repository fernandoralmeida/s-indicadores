
namespace IDN.Tools;

public static class DBConfig
{
    public static readonly string MigraData_RFB = @"migradata_rfb";

    public static readonly string IndicadoresNET = @"www_indicadores";

    public static readonly string DS_POSTGRES
        = Environment.GetEnvironmentVariable("DS_POSTGRES")!;

}