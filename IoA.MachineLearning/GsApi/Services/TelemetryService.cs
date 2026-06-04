using AutoMapper;
using GsApi.Data.DTOS;
using GsApi.Data.Interfaces;
using GsApi.Exceptions;
using GsApi.Model;
using GsApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.VisualBasic;

namespace GsApi.Services;

public class TelemetryPredictionService : ITelemetryPredictionService
{
    private readonly IGenericRepository<PredictionHistory> _repository;
    private readonly IHttpClientFactory _httpClientFactory;

    public TelemetryPredictionService(IGenericRepository<PredictionHistory> repository, IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PredictionResponseDTO> ProcessIncomingDataAsync(TelemetryDTO incomingData)
    {
        try
        {
            // 1. Chamada para a API Python (ML)
            var client = _httpClientFactory.CreateClient("PythonMLApi");
            
            var mlResponse = await client.PostAsJsonAsync("predict-disaster", incomingData);

            if (!mlResponse.IsSuccessStatusCode)
                throw new SpaceTelemetryException($"Falha no Python ML Engine: {mlResponse.StatusCode}");

            var predictionResult = await mlResponse.Content.ReadFromJsonAsync<PredictionResponseDTO>();

            // 2. Criar entidade baseada na interface partial
            var historyRecord = new PredictionHistory
            {
                SpeciesId = incomingData.SpeciesId,
                Latitude = incomingData.Latitude,
                Longitude = incomingData.Longitude,
                Acceleration = incomingData.Acceleration,
                HeartRate = incomingData.HeartRate,
                YoloBirdCount = incomingData.YoloBirdCount,
                YoloMovementIndex = incomingData.YoloMovementIndex,
                PressureHpa = incomingData.PressureHpa,
                TemperatureC = incomingData.TemperatureC,
                PredictedAnomaly = predictionResult!.AnomalyType,
                Probability = predictionResult.Probability,
                AlertLevel = predictionResult.AlertLevel,
                AnalyzedAt = DateTime.UtcNow,
                IsAccurate = null // Atualmente null aguardando validação posterior
            };

            // 3. Salvar no Banco
            await _repository.AddAsync(historyRecord);
            await _repository.SaveChangesAsync();

            // 4. Lógica de Envio de Notificação (Regra 3)
            predictionResult.RequiresImmediateNotification = historyRecord.ShouldNotifyFrontEnd();

            return predictionResult;
        }
        catch (HttpRequestException ex)
        {
            // Captura específica
            throw new SpaceTelemetryException("Conexão perdida com o motor de inferência orbital.", ex);
        }
    }

    public async Task<IEnumerable<PredictionHistory>> GetAllPredictionsAsync()
    {
        return await _repository.GetAllAsync();
    }
}
