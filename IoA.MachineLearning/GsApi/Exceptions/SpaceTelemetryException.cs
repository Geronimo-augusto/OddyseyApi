namespace GsApi.Exceptions;

public class SpaceTelemetryException : Exception
{
    public DateTime ErrorTimestamp { get; private set; }

    public SpaceTelemetryException(string message) : base(message)
    {
        ErrorTimestamp = DateTime.UtcNow;
    }

    public SpaceTelemetryException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorTimestamp = DateTime.UtcNow;
    }
}
