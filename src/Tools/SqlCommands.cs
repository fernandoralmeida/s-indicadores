namespace IDN.Tools;

public static class SqlCommands
{
    public static readonly string Fields_Estabelecimentos = @"(CNPJBase,
                                        CNPJOrdem,
                                        CNPJDV,
                                        IdentificadorMatrizFilial,
                                        NomeFantasia,
                                        SituacaoCadastral,
                                        DataSituacaoCadastral,
                                        MotivoSituacaoCadastral,
                                        NomeCidadeExterior,
                                        Pais,
                                        DataInicioAtividade,
                                        CnaeFiscalPrincipal,
                                        CnaeFiscalSecundaria,
                                        TipoLogradouro,
                                        Logradouro,
                                        Numero,
                                        Complemento,
                                        Bairro,
                                        CEP,
                                        UF,
                                        Municipio,
                                        DDD1,
                                        Telefone1,
                                        DDD2,
                                        Telefone2,
                                        DDDFax,
                                        Fax,
                                        CorreioEletronico,
                                        SituacaoEspecial,
                                        DataSitucaoEspecial)";

    public static readonly string Values_Estabelecimentos = @"(@CNPJBase,
                                        @CNPJOrdem,
                                        @CNPJDV,
                                        @IdentificadorMatrizFilial,
                                        @NomeFantasia,
                                        @SituacaoCadastral,
                                        @DataSituacaoCadastral,
                                        @MotivoSituacaoCadastral,
                                        @NomeCidadeExterior,
                                        @Pais,
                                        @DataInicioAtividade,
                                        @CnaeFiscalPrincipal,
                                        @CnaeFiscalSecundaria,
                                        @TipoLogradouro,
                                        @Logradouro,
                                        @Numero,
                                        @Complemento,
                                        @Bairro,
                                        @CEP,
                                        @UF,
                                        @Municipio,
                                        @DDD1,
                                        @Telefone1,
                                        @DDD2,
                                        @Telefone2,
                                        @DDDFax,
                                        @Fax,
                                        @CorreioEletronico,
                                        @SituacaoEspecial,
                                        @DataSitucaoEspecial)";

    public static readonly string Fields_Empresas = @"(CNPJBase,RazaoSocial,NaturezaJuridica,QualificacaoResponsavel,CapitalSocial,PorteEmpresa,EnteFederativoResponsavel)";
    public static readonly string Values_Empresas = @"(@CNPJBase,@RazaoSocial,@NaturezaJuridica,@QualificacaoResponsavel,@CapitalSocial,@PorteEmpresa,@EnteFederativoResponsavel)";
    public static readonly string Fields_Socios = @"(CNPJBase,IdentificadorSocio,NomeRazaoSocio,CnpjCpfSocio,QualificacaoSocio,DataEntradaSociedade,Pais,RepresentanteLegal,NomeRepresentante,QualificacaoRepresentanteLegal,FaixaEtaria)";
    public static readonly string Values_Socios = @"(@CNPJBase,@IdentificadorSocio,@NomeRazaoSocio,@CnpjCpfSocio,@QualificacaoSocio,@DataEntradaSociedade,@Pais,@RepresentanteLegal,@NomeRepresentante,@QualificacaoRepresentanteLegal,@FaixaEtaria)";
    public static readonly string Fields_Simples = @"(CNPJBase,OpcaoSimples,DataOpcaoSimples,DataExclusaoSimples,OpcaoMEI,DataOpcaoMEI,DataExclusaoMEI)";
    public static readonly string Values_Simples = @"(@CNPJBase,@OpcaoSimples,@DataOpcaoSimples,@DataExclusaoSimples,@OpcaoMEI,@DataOpcaoMEI,@DataExclusaoMEI)";
    public static readonly string Fields_Generic = @"(Codigo,Descricao)";
    public static readonly string Values_Generic = @"(@Codigo,@Descricao)";
    public static readonly string Fields_Indicadores_Empresas = @"(
                                        Id,
                                        CNPJ,
                                        RazaoSocial,
                                        NaturezaJuridica,
                                        CapitalSocial,
                                        PorteEmpresa,
                                        IdentificadorMatrizFilial,
                                        NomeFantasia,
                                        SituacaoCadastral,
                                        DataSituacaoCadastral,
                                        DataInicioAtividade,
                                        CnaeFiscalPrincipal,
                                        CnaeDescricao,
                                        CEP,
                                        Logradouro,
                                        Numero,
                                        Bairro,
                                        UF,
                                        Municipio,
                                        OpcaoSimples,
                                        DataOpcaoSimples,
                                        DataExclusaoSimples,
                                        OpcaoMEI,
                                        DataOpcaoMEI,
                                        DataExclusaoMEI)";
    public static readonly string Values_Indicadores_Empresas = @"(
                                        @Id,
                                        @CNPJ,
                                        @RazaoSocial,
                                        @NaturezaJuridica,
                                        @CapitalSocial,
                                        @PorteEmpresa,
                                        @IdentificadorMatrizFilial,
                                        @NomeFantasia,
                                        @SituacaoCadastral,
                                        @DataSituacaoCadastral,
                                        @DataInicioAtividade,
                                        @CnaeFiscalPrincipal,
                                        @CnaeDescricao,
                                        @CEP,
                                        @Logradouro,
                                        @Numero,
                                        @Bairro,
                                        @UF,
                                        @Municipio,
                                        @OpcaoSimples,
                                        @DataOpcaoSimples,
                                        @DataExclusaoSimples,
                                        @OpcaoMEI,
                                        @DataOpcaoMEI,
                                        @DataExclusaoMEI)";

    public static string DeleteNotExist(string tablename, string tablenameref)
        => $"DELETE FROM {tablename} WHERE NOT EXISTS (SELECT 1 FROM {tablenameref} WHERE {tablenameref}.CNPJBase = {tablename}.CNPJBase)";

    public static string InsertCommand(string tablename, string fields, string values)
        => $"INSERT INTO {tablename} {fields} VALUES {values}";

    public static string DeletCommand(string tablename)
        => $"DELETE FROM {tablename}";

    public static string SelectCommand(string tablename)
        => $"SELECT * FROM {tablename}";

    public static string ViewCommand(string viewname)
        => $"SELECT * FROM {viewname}";

    public static string ViewCommandParam(string viewname)
        => $"SELECT * FROM {viewname} WHERE (Municipio = @Municipio)";

    public static string CreateDataBase(string dbname)
        => $"CREATE DATABASE {dbname}";

    public static string Select_All_Municipio_Indicadores
    => @"SELECT municipio FROM empresas GROUP BY municipio ORDER BY municipio;";


}