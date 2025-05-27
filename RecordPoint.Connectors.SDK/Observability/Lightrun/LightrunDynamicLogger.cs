using Lightrun.Agent.Logging;

namespace RecordPoint.Connectors.SDK.Observability.Lightrun;

/// <summary>
/// Implementation of a Dynmaic Logger for Lightrun using the internal Telemetry Tracker
/// </summary>
/// <param name="telemetryTracker"></param>
internal class LightrunDynamicLogger(ITelemetryTracker telemetryTracker) : IDynamicLogger
{
    /// <summary>
    /// Send Lightrun logs to the Telemetry Tracker
    /// </summary>
    /// <param name="entry"></param>
    public void Log(LogEntry entry)
    {
        var severityLevel = entry.Severity switch
        {
            LogLevel.Debug => SeverityLevel.Verbose,
            LogLevel.Info => SeverityLevel.Information,
            LogLevel.Warning => SeverityLevel.Warning,
            LogLevel.Error => SeverityLevel.Error,
            _ => SeverityLevel.Information
        };

        telemetryTracker.TrackTrace(entry.Message, severityLevel);
    }
}
