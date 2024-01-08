using IDN.Core.Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDN.Data.Config;
public class EmpresaDBMap : IEntityTypeConfiguration<MEmpresa>
{
    public void Configure(EntityTypeBuilder<MEmpresa> builder)
    {
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.CNPJ)
            .HasColumnType("varchar(14)");

        builder
            .Property(c => c.RazaoSocial)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.NaturezaJuridica)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.CapitalSocial)
            .HasColumnType("decimal(18,2)");

        builder
        .Property(c => c.PorteEmpresa)
        .HasColumnType("varchar(2)");

        builder
            .Property(c => c.IdentificadorMatrizFilial)
            .HasColumnType("varchar(1)");

        builder
            .Property(c => c.NomeFantasia)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.SituacaoCadastral)
            .HasColumnType("varchar(2)");

        builder
            .Property(c => c.DataSituacaoCadastral)
            .HasColumnType("varchar(8)");

        builder
            .Property(c => c.DataInicioAtividade)
            .HasColumnType("varchar(8)");

        builder
            .Property(c => c.DataSituacaoCadastral)
            .HasColumnType("varchar(8)");

        builder
            .Property(c => c.CnaeFiscalPrincipal)
            .HasColumnType("varchar(7)");

        builder
            .Property(c => c.CnaeDescricao)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.CEP)
            .HasColumnType("varchar(8)");

        builder
            .Property(c => c.Logradouro)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.Numero)
            .HasColumnType("varchar(10)");

        builder
        .Property(c => c.Bairro)
        .HasColumnType("varchar(255)");

        builder
            .Property(c => c.UF)
            .HasColumnType("varchar(2)");

        builder
        .Property(c => c.Municipio)
        .HasColumnType("varchar(50)");

        builder
            .Property(c => c.OpcaoSimples)
            .HasColumnType("varchar(1)");

        builder
        .Property(c => c.DataOpcaoSimples)
        .HasColumnType("varchar(8)");

        builder
            .Property(c => c.DataExclusaoSimples)
            .HasColumnType("varchar(8)");

        builder
        .Property(c => c.OpcaoMEI)
        .HasColumnType("varchar(1)");

        builder
            .Property(c => c.DataOpcaoMEI)
            .HasColumnType("varchar(8)");

        builder
        .Property(c => c.DataExclusaoMEI)
        .HasColumnType("varchar(8)");





    }
}