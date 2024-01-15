using Microsoft.EntityFrameworkCore.Storage;

namespace IDN.Tools;

class Program
{
    static async Task Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("Tools V1.0");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 Create MigraData_RFB");
            Console.WriteLine("2 Create IndicadoresNet");
            Console.WriteLine("3 Update Estatistics");
            Console.WriteLine("4 Update Geocodes");
            Console.WriteLine("------------------------");
            Console.WriteLine("0 Exit App");
            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    await Companies.CreateMigraData_RFB(DBConfig.MigraData_RFB, DBConfig.DS_POSTGRES);
                    break;

                case 2:
                    await Geojson.GoeCidades();
                    break;

                case 3:
                    await Estatistics.Update();
                    break;

                case 4:
                    await Geojson.GoeCidades();
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