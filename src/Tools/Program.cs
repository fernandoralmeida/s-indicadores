namespace IDN.Tools;

class Program {
    static async Task Main(string[] args){
        
        Console.WriteLine("Olá Mundo!");
        await new Geojson().GoeCidades();   
    }
}