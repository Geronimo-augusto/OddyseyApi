using System;
namespace GsApi.Data.DTOS;

public class PredictionResponseDto
{
    public string AnomalyType { get; set; } = string.Empty;
    public double Probability { get; set; }
    public string AlertLevel { get; set; } = string.Empty;
}
