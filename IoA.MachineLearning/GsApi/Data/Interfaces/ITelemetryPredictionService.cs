using GsApi.Data.DTOS;
using GsApi.Model;


namespace GsApi.Data.Interfaces;

public interface ITelemetryPredictionService
{
    Task<PredictionResponseDTO> ProcessIncomingDataAsync(TelemetryDTO incomingData);
    Task<IEnumerable<PredictionHistory>> GetAllPredictionsAsync();
}