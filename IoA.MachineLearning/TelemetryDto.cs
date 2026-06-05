using System;

namespace GsApi.Data.DTOS;
public class Telemetry
{
    public string SpeciesId { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Acceleration { get; set; }
    public double HeartRate { get; set; }
}
