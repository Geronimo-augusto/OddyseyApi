using OddyseyApi.Data.DTOS;
using OddyseyApi.Model;


namespace OddyseyApi.Data.Interfaces;

public interface ITelemetryPredictionService
{
    Task<PredictionResponseDTO> ProcessIncomingDataAsync(TelemetryDTO incomingData);
    Task<IEnumerable<PredictionHistory>> GetAllPredictionsAsync();
}