using System;
namespace OddyseyApi.Models;
public class Telemetry
{

	public Guid Id { get; set; } = Guid.NewGuid();
    public string SpeciesId { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Acceleration { get; set; }
    public double HeartRate { get; set; }
    public DateTime CollectedAt { get; set; } = DateTime.UtcNow;
	
}
