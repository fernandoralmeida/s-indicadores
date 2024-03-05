using IDN.Core.Cnae.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDN.Data.Config;
public class CnaeDBMap : IEntityTypeConfiguration<MCnae>
{
    public void Configure(EntityTypeBuilder<MCnae> builder)
    {
        builder.HasNoKey();
        builder
            .Property(c => c.Codigo)
            .HasColumnType("varchar(10)")
            .HasColumnName("codigo");

        builder
            .Property(c => c.Descricao)
            .HasColumnType("varchar(8000)")
            .HasColumnName("descricao");

        
    }
}