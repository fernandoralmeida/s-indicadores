IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Empresas] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] varchar(14) NULL,
    [RazaoSocial] varchar(255) NULL,
    [NaturezaJuridica] varchar(255) NULL,
    [CapitalSocial] decimal(18,2) NULL,
    [PorteEmpresa] varchar(2) NULL,
    [IdentificadorMatrizFilial] varchar(1) NULL,
    [NomeFantasia] varchar(255) NULL,
    [SituacaoCadastral] varchar(2) NULL,
    [DataSituacaoCadastral] varchar(8) NULL,
    [DataInicioAtividade] varchar(8) NULL,
    [CnaeFiscalPrincipal] varchar(7) NULL,
    [CnaeDescricao] varchar(255) NULL,
    [Logradouro] varchar(255) NULL,
    [Numero] varchar(10) NULL,
    [Bairro] varchar(255) NULL,
    [CEP] varchar(8) NULL,
    [UF] varchar(2) NULL,
    [Municipio] varchar(50) NULL,
    [OpcaoSimples] varchar(1) NULL,
    [DataOpcaoSimples] varchar(8) NULL,
    [DataExclusaoSimples] varchar(8) NULL,
    [OpcaoMEI] varchar(1) NULL,
    [DataOpcaoMEI] varchar(8) NULL,
    [DataExclusaoMEI] varchar(8) NULL,
    CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Municipios] (
    [Codigo] nvarchar(max) NULL,
    [Descricao] nvarchar(max) NULL
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231017173433_startdb', N'7.0.11');
GO

COMMIT;
GO

