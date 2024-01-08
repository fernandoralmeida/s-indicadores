using IDN.Core.Municipio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDN.Data.Config;
public class MunicipioDBMap : IEntityTypeConfiguration<MMunicipio>
{
    public void Configure(EntityTypeBuilder<MMunicipio> builder)
    {
        builder.HasNoKey();
    }
}