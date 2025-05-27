using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Diagnostics;
using System;

namespace RecordPoint.Connectors.SDK.R365;

/// <summary>
/// The SDK log adapter.
/// </summary>
public class SDKLogAdapter(ITelemetryTracker telemetryTracker) : ILog
{
    /// <summary>
    /// Log the message.
    /// </summary>
    /// <param name="callerType">The caller type.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="message">The message.</param>
    /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
    public void LogMessage(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
    {
        telemetryTracker.TrackTrace($"Message::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]", SeverityLevel.Information);
    }

    /// <summary>
    /// Log the verbose.
    /// </summary>
    /// <param name="callerType">The caller type.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="message">The message.</param>
    /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
    public void LogVerbose(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
    {
        telemetryTracker.TrackTrace($"Verbose::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]", SeverityLevel.Verbose);
    }

    /// <summary>
    /// Log the warning.
    /// </summary>
    /// <param name="callerType">The caller type.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="message">The message.</param>
    /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
    public void LogWarning(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
    {
        telemetryTracker.TrackTrace($"Warning::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]", SeverityLevel.Warning);
    }
}
