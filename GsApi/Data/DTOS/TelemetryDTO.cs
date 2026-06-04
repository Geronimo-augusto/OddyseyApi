using AutoMapper.Internal.Mappers;
using System;

namespace OddyseyApi.Data.DTOS;

public class TelemetryDTO
{
    public string SpeciesId { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Acceleration { get; set; }
    public double HeartRate { get; set; }

    // Novos campos vindos da Câmera (YOLO) e da API de Clima
    public int YoloBirdCount { get; set; }
    public double YoloMovementIndex { get; set; }
    public double PressureHpa { get; set; }
    public double TemperatureC { get; set; }
}
