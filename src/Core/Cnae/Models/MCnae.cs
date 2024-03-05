using System.ComponentModel.DataAnnotations.Schema;

namespace IDN.Core.Cnae.Models;

public class MCnae
{
    public MCnae()
    {
        
    }

    [Column(TypeName = "citext")]
    public string? Codigo { get; set; }

    [Column(TypeName = "citext")]
    public string? Descricao { get; set; }

}