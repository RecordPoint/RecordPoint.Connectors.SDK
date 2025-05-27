namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Interface for submitting a variety of statistics and measures about the system in a consistent and structured manner.
/// </summary>
public interface ITelemetrySink
{
    /// <summary>
    /// Tracks an event with the given name, dimensions, and measures.
    /// </summary>
    /// <param name="name">The name of the Event to track</param>
    /// <param name="dimensions">Dimensions to include in the event</param>
    /// <param name="measures">Measures to include in the event</param>
    void TrackEvent(string name, Dimensions? dimensions = null, Measures? measures = null);

    /// <summary>
    /// Tracks a trace message with the given severity level, message, and dimensions.
    /// </summary>
    /// <param name="message">The message to trace</param>
    /// <param name="severityLevel">The severity level of the trace</param>
    /// <param name="dimensions">Dimensions to include in the trace</param>
    void TrackTrace(string message, SeverityLevel severityLevel, Dimensions? dimensions = null);

    /// <summary>
    /// Tracks an exception with the given dimensions and measures.
    /// </summary>
    /// <param name="exception">The exception to track</param>
    /// <param name="dimensions">Dimensions to include with the exception</param>
    /// <param name="measures">Measures to include with the exception</param>
    void TrackException(Exception exception, Dimensions? dimensions = null, Measures? measures = null);
}
