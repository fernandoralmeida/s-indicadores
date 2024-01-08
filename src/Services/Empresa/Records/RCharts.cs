namespace IDN.Services.Empresa.Records;

public record RCharts(
    string Rotatividade,
    string[] NovasMes,
    string[] BaixasMes,
    string[] MatrizFilial,
    string[] MatrizFilial_Ano,
    string[] Fiscal,
    string[] Fiscal_Ano,
    string[] PorteFiscal,
    string[] PorteFiscalAno,
    string[] LabelPorteFiscal,
    string Maturidade,
    string[] Setores,
    string[] SetoresAno,
    string[] CrescimentoSetorial,
    IEnumerable<string> Setores_Controle
);