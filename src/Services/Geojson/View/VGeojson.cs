using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;

namespace IDN.Services.Geojson.View;

public class VGeojson
{
    public string? Type { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    public IEnumerable<VFeatures>? Features { get; set; }
}

public class VFeatures
{
    public Guid _id { get; set; }
    public string? Type { get; set; }
    public VGeometry? Geometry { get; set; }
    public VProperties? Properties { get; set; }
}

public class VGeometry
{
    public string? Type { get; set; }
    public IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>? Coordinates { get; set; }
}

public class VProperties
{
    public string? Name { get; set; }
    public string? Geocode { get; set; }
    public int Empresas { get; set; }
    public string? Setor { get; set; }
}