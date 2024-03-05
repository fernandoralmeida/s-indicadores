namespace IDN.Tools;

class Program
{
    static async Task Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("Tools V1.0");
            Console.WriteLine("------------------------");
            Console.WriteLine("Options");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 Normalize Files");
            Console.WriteLine("------------------------");
            Console.WriteLine("2 Create MigraData_RFB");
            Console.WriteLine("3 Create IndicadoresNet");
            Console.WriteLine("4 Create IndicadoresNet -> VPS");
            Console.WriteLine("------------------------");
            Console.WriteLine("5 Update Estatistics");
            Console.WriteLine("6 Update Geocodes");
            Console.WriteLine("------------------------");
            Console.WriteLine("7 Do All Services [1 -> 6]");
            Console.WriteLine("------------------------");
            Console.WriteLine("8 Create CNAEs");
            Console.WriteLine("------------------------");
            Console.WriteLine("0 Exit App");
            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    await FilesCsv.NormalizeAsync();
                    break;
                case 2:
                    await Data.CreateDataBase(SqlScript.MIGRADATA_RFB, DBConfig.DS_POSTGRES);
                    await Data.CreateTables(SqlScript.MIGRADATA_RFB_TABLES, DBConfig.DS_POSTGRES, DBConfig.MigraData_RFB);

                    await Migradata.DoCNAE(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoMotivo(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoMunicipios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoNaturezaJuridica(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoPaises(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoQualificacaoSocios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);

                    await Migradata.DoEstabelecimentos(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoEmpresas(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoSocios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoSimples(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    break;

                case 3:
                    await Data.CreateDataBase(SqlScript.WWW_INDICADORES, DBConfig.DS_POSTGRES);
                    await Data.CreateTables(SqlScript.WWW_INDICADORES_TABLES, DBConfig.DS_POSTGRES, DBConfig.IndicadoresNET);
                    await Indicadores.DoIndicadores(DBConfig.DS_POSTGRES, DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES, DBConfig.IndicadoresNET);
                    break;

                case 4:
                    await Data.CreateDataBase(SqlScript.WWW_INDICADORES, DBConfig.DS_POSTGRES_VPS);
                    await Data.CreateTables(SqlScript.WWW_INDICADORES_TABLES, DBConfig.DS_POSTGRES_VPS, DBConfig.IndicadoresNET);
                    await Indicadores.DoIndicadores(DBConfig.DS_POSTGRES, DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES_VPS, DBConfig.IndicadoresNET);
                    break;

                case 5:
                    await Estatistics.Update();
                    break;

                case 6:
                    await Geojson.GoeCidades();
                    break;

                case 7:
                    await FilesCsv.NormalizeAsync();
                    await Data.CreateDataBase(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);

                    await Migradata.DoCNAE(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoMotivo(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoMunicipios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoNaturezaJuridica(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoPaises(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoQualificacaoSocios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);

                    await Migradata.DoEstabelecimentos(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoEmpresas(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoSocios(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    await Migradata.DoSimples(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);

                    await Data.CreateDataBase(DBConfig.IndicadoresNET, DBConfig.DS_POSTGRES);
                    await Indicadores.DoIndicadores(DBConfig.DS_POSTGRES, DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES, DBConfig.IndicadoresNET);

                    await Data.CreateDataBase(DBConfig.IndicadoresNET, DBConfig.DS_POSTGRES_VPS);
                    await Indicadores.DoIndicadores(DBConfig.DS_POSTGRES, DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES_VPS, DBConfig.IndicadoresNET);

                    await Estatistics.Update();
                    await Geojson.GoeCidades();
                    break;

                case 8:
                    await Data.CreateTables(SqlScript.CNAE_TABLE, DBConfig.DS_POSTGRES, DBConfig.IndicadoresNET);
                    await Migradata.DoCNAE(DBConfig.IndicadoresNET, DBConfig.DS_POSTGRES);
                    break;

                case 0:
                    Console.WriteLine("Closing App...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again!");
                    break;
            }
        }

    }
}