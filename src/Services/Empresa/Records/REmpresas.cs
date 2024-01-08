using IDN.Services.Municipio.View;

namespace IDN.Services.Empresa.Records;

public record REmpresas(
    string Municipio,
    IEnumerable<KeyValuePair<string, int>> Quantitativo,
    IEnumerable<KeyValuePair<string, int>> Quantitativo_Ano,
    IEnumerable<KeyValuePair<string, int>> NovasEmpresas,
    IEnumerable<KeyValuePair<string, int>> NovasEmpresas_Ano,
    IEnumerable<KeyValuePair<string, int>> MatrizFilial,
    IEnumerable<KeyValuePair<string, int>> MatrizFilial_Ano,
    IEnumerable<KeyValuePair<string, int>> Baixas_Ano,
    IEnumerable<KeyValuePair<string, int>> NaturezaJuridica_Ano,
    IEnumerable<KeyValuePair<string, int>> NaturezaJuridica,
    IEnumerable<KeyValuePair<string, int>> Setores,
    IEnumerable<KeyValuePair<string, int>> Setores_Ano,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> TAtividades,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> Top3Atividades,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> Top3Atividades_Ano,
    IEnumerable<KeyValuePair<string, int>> Fiscal,
    IEnumerable<KeyValuePair<string, int>> Fiscal_Ano,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> Porte,
    IEnumerable<KeyValuePair<string, int>> PorteS,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> Porte_Ano,
    IEnumerable<KeyValuePair<string, int>> Simples,
    IEnumerable<KeyValuePair<string, int>> Simples_Ano,
    IEnumerable<KeyValuePair<string, int>> Idade,
    IEnumerable<KeyValuePair<string, float>> Rotatividade,
    IEnumerable<KeyValuePair<string, int>> EmpresasPorLocal,
    IEnumerable<KeyValuePair<string, int>> EmpresasNovasPorLocal,
    IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> TaxaCrescimentoSetorial
);



