namespace IDN.Core.Municipio.Models;

public class MMunicipio
{
    public MMunicipio()
    {
        
    }
    public string? Codigo { get; set; }

    public string? Descricao { get; set; }

    public bool MicroRegiaoJahu(MMunicipio obj)
    {
        return
            obj.Codigo == "6607" ||
            obj.Codigo == "6501" ||
            obj.Codigo == "6235" ||
            obj.Codigo == "6559" ||
            obj.Codigo == "6245" ||
            obj.Codigo == "6203" ||
            obj.Codigo == "6541" ||
            obj.Codigo == "6205" ||
            obj.Codigo == "7195" ||
            obj.Codigo == "6259" ||
            obj.Codigo == "6697" ||
            obj.Codigo == "6835" ||
            obj.Codigo == "6383" ||
            obj.Codigo == "6219" ||
            obj.Codigo == "6249";
    }
}