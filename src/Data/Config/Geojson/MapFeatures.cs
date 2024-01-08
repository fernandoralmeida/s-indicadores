using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IDN.Core.Geojson.Models;

namespace IDN.Data.Config.Geojson;
public class MapFeatures : IEntityTypeConfiguration<MFeatures>
{
    public void Configure(EntityTypeBuilder<MFeatures> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Type)
            .HasColumnType("varchar(255)");
    }
}