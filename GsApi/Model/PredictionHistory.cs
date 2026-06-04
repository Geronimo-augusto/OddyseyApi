namespace OddyseyApi.Model;

public partial class PredictionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Dados brutos recebidos
    public string SpeciesId { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Acceleration { get; set; }
    public double HeartRate { get; set; }

    public int YoloBirdCount { get; set; }
    public double YoloMovementIndex { get; set; }
    public double PressureHpa { get; set; }
    public double TemperatureC { get; set; }

    // Resultado da Previsão
    public string PredictedAnomaly { get; set; } = string.Empty;
    public double Probability { get; set; }
    public string AlertLevel { get; set; } = string.Empty;

    // Manipulação precisa de datas
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

    // Regra de negócio 2: Pode ser null inicialmente
    public bool? IsAccurate { get; set; }
}

