namespace GsApi.Model;

public partial class PredictionHistory
{
    // Uso de método privado/interno para encapsular regra de notificação (Regra de negócio 3)
    public bool ShouldNotifyFrontEnd()
    {
        // Notifica apenas se a certeza for crítica e maior que 85%
        return AlertLevel == "CRITICAL" && Probability > 0.85;
    }
}
