using GsApi.Data.DTOS;
using GsApi.Data.Interfaces;
using GsApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;

namespace GsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryPredictionService _predictionService;

    public TelemetryController(ITelemetryPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    // POST: api/telemetry/predict
    [HttpPost("predict")]
    public async Task<IActionResult> ReceiveAndPredict([FromBody] TelemetryDTO payload)
    {
        try
        {
            var result = await _predictionService.ProcessIncomingDataAsync(payload);

            // Se o nível for crítico, pode retornar 201 ou 200 com flag
            return Ok(new { message = "Telemetria processada.", data = result });
        }
        catch (SpaceTelemetryException ex)
        {
            // Sistema crítico falhando graciosamente
            return StatusCode(503, new { error = ex.Message, timestamp = ex.ErrorTimestamp });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro não catalogado nos subsistemas de alerta."});
        }
    }

    // GET: api/telemetry/predictions
    [HttpGet("predictions")]
    public async Task<IActionResult> GetPredictionsHistory()
    {
        var history = await _predictionService.GetAllPredictionsAsync();
        return Ok(history);
    }
}