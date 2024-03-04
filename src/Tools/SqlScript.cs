namespace IDN.Tools;

//Postgres v16
public static class SqlScript
{
    public static string MIGRADATA_RFB
    => @"
-- always create a new db migradata_rfb

-- drop old db
DROP DATABASE IF EXISTS migradata_rfb;

-- create a new db
CREATE DATABASE migradata_rfb
    ";
    public static string MIGRADATA_RFB_TABLES
    => @"
-- create tables
CREATE TABLE Cnaes (
    Codigo VARCHAR(10) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE Empresas (
    CNPJBase VARCHAR(8) NULL,
    RazaoSocial VARCHAR(255) NULL,
    NaturezaJuridica VARCHAR(5) NULL,
    QualificacaoResponsavel VARCHAR(5) NULL,
    CapitalSocial VARCHAR(20) NULL,
    PorteEmpresa VARCHAR(5) NULL,
    EnteFederativoResponsavel VARCHAR(255) NULL
);

CREATE TABLE Estabelecimentos (
    CNPJBase VARCHAR(8) NULL,
    CNPJOrdem VARCHAR(4) NULL,
    CNPJDV VARCHAR(2) NULL,
    IdentificadorMatrizFilial VARCHAR(2) NULL,
    NomeFantasia VARCHAR(255) NULL,
    SituacaoCadastral VARCHAR(2) NULL,
    DataSituacaoCadastral VARCHAR(10) NULL,
    MotivoSituacaoCadastral VARCHAR(2) NULL,
    NomeCidadeExterior VARCHAR(255) NULL,
    Pais VARCHAR(5) NULL,
    DataInicioAtividade VARCHAR(10) NULL,
    CnaeFiscalPrincipal VARCHAR(10) NULL,
    CnaeFiscalSecundaria varchar(8000) NULL,
    TipoLogradouro VARCHAR(255) NULL,
    Logradouro VARCHAR(255) NULL,
    Numero VARCHAR(255) NULL,
    Complemento VARCHAR(255) NULL,
    Bairro VARCHAR(255) NULL,
    CEP VARCHAR(10) NULL,
    UF VARCHAR(2) NULL,
    Municipio VARCHAR(10) NULL,
    DDD1 VARCHAR(4) NULL,
    Telefone1 VARCHAR(255) NULL,
    DDD2 VARCHAR(4) NULL,
    Telefone2 VARCHAR(255) NULL,
    DDDFax VARCHAR(4) NULL,
    Fax VARCHAR(255) NULL,
    CorreioEletronico VARCHAR(255) NULL,
    SituacaoEspecial VARCHAR(50) NULL,
    DataSitucaoEspecial VARCHAR(10) NULL
);

CREATE TABLE MotivoSituacaoCadastral (
    Codigo VARCHAR(2) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE Municipios (
    Codigo VARCHAR(10) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE NaturezaJuridica (
    Codigo VARCHAR(5) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE Paises (
    Codigo VARCHAR(5) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE QualificacaoSocios (
    Codigo VARCHAR(4) NULL,
    Descricao varchar(8000) NULL
);

CREATE TABLE Simples (
    CNPJBase VARCHAR(8) NULL,
    OpcaoSimples VARCHAR(2) NULL,
    DataOpcaoSimples VARCHAR(10) NULL,
    DataExclusaoSimples VARCHAR(10) NULL,
    OpcaoMEI VARCHAR(2) NULL,
    DataOpcaoMEI VARCHAR(10) NULL,
    DataExclusaoMEI VARCHAR(10) NULL
);

CREATE TABLE Socios (
    CNPJBase VARCHAR(8) NULL,
    IdentificadorSocio VARCHAR(2) NULL,
    NomeRazaoSocio VARCHAR(255) NULL,
    CnpjCpfSocio VARCHAR(15) NULL,
    QualificacaoSocio VARCHAR(4) NULL,
    DataEntradaSociedade VARCHAR(10) NULL,
    Pais VARCHAR(5) NULL,
    RepresentanteLegal VARCHAR(50) NULL,
    NomeRepresentante VARCHAR(255) NULL,
    QualificacaoRepresentanteLegal VARCHAR(4) NULL,
    FaixaEtaria VARCHAR(2) NULL
);

-- create views
CREATE OR REPLACE VIEW public.view_empresas_by_municipio
 AS
 SELECT (emp.cnpjbase::text || est.cnpjdv::text) || est.cnpjordem::text AS cnpj,
    emp.razaosocial,
    (njd.codigo::text || ' '::text) || njd.descricao::text AS naturezajuridica,
    emp.capitalsocial,
    emp.porteempresa,
    est.identificadormatrizfilial,
    est.nomefantasia,
    est.situacaocadastral,
    est.datasituacaocadastral,
    est.datainicioatividade,
    est.cnaefiscalprincipal,
    atv.descricao AS cnaedescricao,
    est.cep,
    (est.tipologradouro::text || ' '::text) || est.logradouro::text AS logradouro,
    est.numero,
    est.bairro,
    est.uf,
    mps.descricao AS municipio,
    snl.opcaosimples,
    snl.dataopcaosimples,
    snl.dataexclusaosimples,
    snl.opcaomei,
    snl.dataopcaomei,
    snl.dataexclusaomei
   FROM estabelecimentos est
     JOIN empresas emp ON est.cnpjbase::text = emp.cnpjbase::text
     JOIN cnaes atv ON est.cnaefiscalprincipal::text = atv.codigo::text
     JOIN naturezajuridica njd ON emp.naturezajuridica::text = njd.codigo::text
     JOIN municipios mps ON est.municipio::text = mps.codigo::text
     LEFT JOIN simples snl ON est.cnpjbase::text = snl.cnpjbase::text;

ALTER TABLE public.view_empresas_by_municipio
    OWNER TO postgres;

CREATE OR REPLACE VIEW public.view_municipios
 AS
 SELECT mps.descricao AS municipio
   FROM estabelecimentos est
     JOIN municipios mps ON est.municipio::text = mps.codigo::text
	 GROUP BY mps.descricao;

ALTER TABLE public.view_municipios
    OWNER TO postgres;

-- create indexs
CREATE INDEX idx_empresas_cnpjbase ON empresas (CNPJBase);
CREATE INDEX idx_estabelecimentosa_cnpjbase ON estabelecimentos (CNPJBase);
CREATE INDEX idx_simples_cnpjbase ON simples (CNPJBase);
CREATE INDEX idx_socios_cnpjbase ON socios (CNPJBase);
";

    public static string WWW_INDICADORES
    => @"
-- always create a new db www_indicadores

-- drop old db
DROP DATABASE IF EXISTS www_indicadores;

-- create a new db
CREATE DATABASE www_indicadores;
    ";

    public static string WWW_INDICADORES_TABLES =>
    @"
-- create tables
CREATE TABLE Empresas (    
    CNPJ varchar(14) NULL,
    RazaoSocial varchar(255) NULL,
    NaturezaJuridica varchar(255) NULL,
    CapitalSocial varchar(255) NULL,
    PorteEmpresa varchar(2) NULL,
    IdentificadorMatrizFilial varchar(1) NULL,
    NomeFantasia varchar(255) NULL,
    SituacaoCadastral varchar(2) NULL,
    DataSituacaoCadastral varchar(8) NULL,
    DataInicioAtividade varchar(8) NULL,
    CnaeFiscalPrincipal varchar(7) NULL,
    CnaeDescricao varchar(255) NULL,
    CEP varchar(255) NULL,
    Logradouro varchar(255) NULL,
    Numero varchar(255) NULL,
    Bairro varchar(255) NULL,
    UF varchar(2) NULL,
    Municipio varchar(50) NULL,
    OpcaoSimples varchar(1) NULL,
    DataOpcaoSimples varchar(8) NULL,
    DataExclusaoSimples varchar(8) NULL,
    OpcaoMEI varchar(1) NULL,
    DataOpcaoMEI varchar(8) NULL,
    DataExclusaoMEI varchar(8) NULL    
);

-- create indexs
CREATE INDEX idx_empresas_municipio ON empresas (municipio);

";
}