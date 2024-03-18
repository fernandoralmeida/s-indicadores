using IDN.Core.Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDN.Data.Config;
public class EmpresaDBMap : IEntityTypeConfiguration<MEmpresa>
{
    public void Configure(EntityTypeBuilder<MEmpresa> builder)
    {        
        builder.HasNoKey();
        builder
            .Property(c => c.CNPJ)
            .HasColumnType("varchar(14)")
            .HasColumnName("cnpj");

        builder
            .Property(c => c.RazaoSocial)
            .HasColumnType("varchar(255)")
            .HasColumnName("razaosocial");

        builder
            .Property(c => c.NaturezaJuridica)
            .HasColumnType("varchar(255)")
            .HasColumnName("naturezajuridica");

        builder
            .Property(c => c.CapitalSocial)
            .HasColumnType("varchar(255)")
            .HasColumnName("capitalsocial");

        builder
        .Property(c => c.PorteEmpresa)
        .HasColumnType("varchar(2)")
        .HasColumnName("porteempresa");

        builder
            .Property(c => c.IdentificadorMatrizFilial)
            .HasColumnType("varchar(1)")
            .HasColumnName("identificadormatrizfilial");

        builder
            .Property(c => c.NomeFantasia)
            .HasColumnType("varchar(255)")
            .HasColumnName("nomefantasia");

        builder
            .Property(c => c.SituacaoCadastral)
            .HasColumnType("varchar(2)")
            .HasColumnName("situacaocadastral");

        builder
            .Property(c => c.DataSituacaoCadastral)
            .HasColumnType("varchar(8)")
            .HasColumnName("datasituacaocadastral");

        builder
            .Property(c => c.DataInicioAtividade)
            .HasColumnType("varchar(8)")
            .HasColumnName("datainicioatividade");

        builder
            .Property(c => c.DataSituacaoCadastral)
            .HasColumnType("varchar(8)")
            .HasColumnName("datasituacaocadastral");

        builder
            .Property(c => c.CnaeFiscalPrincipal)
            .HasColumnType("varchar(7)")
            .HasColumnName("cnaefiscalprincipal");

        builder
            .Property(c => c.CnaeDescricao)
            .HasColumnType("varchar(255)")
            .HasColumnName("cnaedescricao");

        builder
            .Property(c => c.CEP)
            .HasColumnType("varchar(8)")
            .HasColumnName("cep");

        builder
            .Property(c => c.Logradouro)
            .HasColumnType("varchar(255)")
            .HasColumnName("logradouro");

        builder
            .Property(c => c.Numero)
            .HasColumnType("varchar(10)")
            .HasColumnName("numero");

        builder
        .Property(c => c.Bairro)
        .HasColumnType("varchar(255)")
        .HasColumnName("bairro");

        builder
            .Property(c => c.UF)
            .HasColumnType("varchar(2)")
            .HasColumnName("uf");

        builder
        .Property(c => c.Municipio)
        .HasColumnType("varchar(50)")
        .HasColumnName("municipio");

        builder
            .Property(c => c.OpcaoSimples)
            .HasColumnType("varchar(1)")
            .HasColumnName("opcaosimples");

        builder
        .Property(c => c.DataOpcaoSimples)
        .HasColumnType("varchar(8)")
        .HasColumnName("dataopcaosimples");

        builder
            .Property(c => c.DataExclusaoSimples)
            .HasColumnType("varchar(8)")
            .HasColumnName("dataexclusaosimples");

        builder
        .Property(c => c.OpcaoMEI)
        .HasColumnType("varchar(1)")
        .HasColumnName("opcaomei");

        builder
            .Property(c => c.DataOpcaoMEI)
            .HasColumnType("varchar(8)")
            .HasColumnName("dataopcaomei");

        builder
        .Property(c => c.DataExclusaoMEI)
        .HasColumnType("varchar(8)")
        .HasColumnName("dataexclusaomei");
    }
}