using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IDN.Core.Geojson.Models;

namespace IDN.Data.Config.Geojson;

public class MapGeometry : IEntityTypeConfiguration<MGeometry>
{
    public void Configure(EntityTypeBuilder<MGeometry> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Type)
            .HasColumnType("varchar(255)");
    }
}