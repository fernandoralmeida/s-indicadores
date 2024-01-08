namespace IDN.Core.Geojson.Models;

public class MGeojson
{
    public string? Type { get; set; }
    public IEnumerable<MFeatures>? Features { get; set; }
}

public class MFeatures
{
    public MFeatures(){}
    public Guid? Id { get; set; }
    public string? Type { get; set; }
    public MGeometry? Geometry { get; set; }
    public MProperties? Properties { get; set; }
}

public class MGeometry
{
    public MGeometry(){}
    public Guid? Id { get; set; }
    public string? Type { get; set; }
    public IEnumerable<MCoordinates>? Coordinates { get; set; }
}

public class MCoordinates
{
    public MCoordinates(){}
    public Guid? Id { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}

public class MProperties
{
    public MProperties(){}
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Geocode { get; set; }
}