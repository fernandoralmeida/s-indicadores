namespace IDN.Services.Empresa.Records;

public record RCharts(
    string Municipio,
    string Rotatividade,
    string[] NovasMes,
    string[] NovasMeiMes,
    string[] NovasMEMes,
    string[] NovasEPPMes,
    string[] NovasDemaisMes,
    string[] BaixasMes,
    string[] MatrizFilial,
    string[] MatrizFilial_Ano,
    string[] NaturezaJuridica,
    string[] NaturezaJuridica_Ano,
    string[] Fiscal,
    string[] Fiscal_Ano,
    string[] PorteFiscal,
    string[] PorteFiscalAno,
    string[] LabelPorteFiscal,
    string Maturidade,
    string[] Setores,
    string[] SetoresAno,
    string[] CrescimentoSetorial,
    string[] TxCrescimentoSetorial,
    IEnumerable<string> Setores_Controle
);