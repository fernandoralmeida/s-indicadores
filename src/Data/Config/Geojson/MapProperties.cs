using IDN.Core.Geojson.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDN.Data.Config.Geojson;

public class MapProperties : IEntityTypeConfiguration<MProperties>
{
    public void Configure(EntityTypeBuilder<MProperties> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .HasColumnType("varchar(255)");

        builder
            .Property(c => c.Geocode)
            .HasColumnType("varchar(255)");
    }
}