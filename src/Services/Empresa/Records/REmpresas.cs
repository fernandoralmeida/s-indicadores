using IDN.Services.Municipio.View;

namespace IDN.Services.Empresa.Records;

public record REmpresas
{
    public Guid _id { get; set; }
    public string? Municipio { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Quantitativo { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Quantitativo_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? NovasEmpresas { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? NovasEmpresas_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? MatrizFilial { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? MatrizFilial_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Baixas_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? NaturezaJuridica_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? NaturezaJuridica { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Setores { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Setores_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? TAtividades { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? TAtividadesDescritivas { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? Top3Atividades { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? Top3Atividades_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Fiscal { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Fiscal_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? Porte { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? PorteS { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? Porte_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Simples { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Simples_Ano { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Idade { get; set; }
    public IEnumerable<KeyValuePair<string, float>>? Rotatividade { get; set; }    
    public IEnumerable<KeyValuePair<string, int>>? EmpresasPorLocal { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? EmpresasNovasPorLocal { get; set; }
    public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>? TaxaCrescimentoSetorial { get; set; }
}





