using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using IDN.Core.Helpers;

namespace IDN.Core.Empresa.Models;

public class MEmpresa
{

    public MEmpresa()
    { }

    private string _situacaocadastral = string.Empty;
    private string _datainicioatividade = string.Empty;
    private string _datasituacaocadastral = string.Empty;
    private string _matriz = string.Empty;
    private string _porte = string.Empty;
    private string _dataopcaosn = string.Empty;
    private string _dataopcaomei = string.Empty;
    private string _dataexclusaosn = string.Empty;
    private string _dataexclusaomei = string.Empty;
    public Guid Id { get; set; }
    public string? CNPJ { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NaturezaJuridica { get; set; }
    public decimal? CapitalSocial { get; set; }
    public string? PorteEmpresa
    {
        get { return Dictionaries.PorteEmpresa[_porte]; }
        set { _porte = value!; }
    }
    public string? IdentificadorMatrizFilial
    {
        get { return Dictionaries.MatrizOuFilial[_matriz]; }
        set { _matriz = value!; }
    }
    public string? NomeFantasia { get; set; }
    public string? SituacaoCadastral
    {
        get { return Dictionaries.SituacaoCadastral[_situacaocadastral]; }
        set { _situacaocadastral = value!; }
    }
    public string? DataSituacaoCadastral
    {
        get { return _datasituacaocadastral.StringDateTime(); }
        set { _datasituacaocadastral = value!; }
    }
    public string? DataInicioAtividade
    {
        get { return _datainicioatividade.StringDateTime(); }
        set { _datainicioatividade = value!; }
    }
    public string? CnaeFiscalPrincipal { get; set; }
    public string? CnaeDescricao { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? CEP { get; set; }
    public string? UF { get; set; }
    public string? Municipio { get; set; }
    public string? OpcaoSimples { get; set; }
    public string? DataOpcaoSimples
    {
        get { return _dataopcaosn.StringDateTime(); }
        set { _dataopcaosn = value!; }
    }
    public string? DataExclusaoSimples
    {
        get { return _dataexclusaosn.StringDateTime(); }
        set { _dataexclusaosn = value!; }
    }
    public string? OpcaoMEI { get; set; }
    public string? DataOpcaoMEI
    {
        get { return _dataopcaomei.StringDateTime(); }
        set { _dataopcaomei = value!; }
    }
    public string? DataExclusaoMEI
    {
        get { return _dataexclusaomei.StringDateTime(); }
        set { _dataexclusaomei = value!; }
    }
    public string SetorProdutivo()
    => Dictionaries.SetorProdutivo[CnaeFiscalPrincipal![..2]];

    public string RegimeFiscal()
    => OpcaoMEI switch
    {
        "S" => "MEI",
        "N" => OpcaoSimples switch
        {
            "S" => "Simples",
            _ => "Outros",
        },
        _ => "Outros",
    };

    public string Localizacao()
    => $"{Logradouro},{Numero},{Municipio},{UF},BRASIL";
}