namespace IDN.Services.Geojson.View;

public class VGeojson
{
    public string? Type { get; set; }
    public IEnumerable<VFeatures>? Features { get; set; }
}

public class VFeatures
{
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
}