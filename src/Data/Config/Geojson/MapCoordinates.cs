using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IDN.Core.Geojson.Models;

namespace IDN.Data.Config.Geojson;
public class MapCoordinates : IEntityTypeConfiguration<MCoordinates>
{
    public void Configure(EntityTypeBuilder<MCoordinates> builder)
    {
        builder
            .HasKey(c => c.Id);
    }
}