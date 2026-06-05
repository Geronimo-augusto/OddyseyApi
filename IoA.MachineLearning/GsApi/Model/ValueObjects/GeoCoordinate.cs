namespace GsApi.Model.ValueObjects;

public class GeoCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }
    public GeoCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
