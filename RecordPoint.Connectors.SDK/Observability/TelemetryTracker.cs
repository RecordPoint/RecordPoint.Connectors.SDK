using RecordPoint.Connectors.SDK.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Implementation of a Telemetry Tracker
/// </summary>
public class TelemetryTracker(IObservabilityScope observabilityScope, IEnumerable<ITelemetrySink> telemetrySinks, ISystemContext systemContext) : ITelemetryTracker
{
    /// <summary>
    /// Begin a new Observability Scope
    /// </summary>
    public IDisposable BeginScope(Dimensions dimensions = null, Measures measures = null)
    {
        return observabilityScope.BeginScope(dimensions, measures);
    }

    /// <summary>
    /// Track a custom event across all registered telemetry sinks
    /// </summary>
    public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
    {
        var gatheredDimensions = GatherDimensions(dimensions, null);
        var gatheredMeasures = GatherMeasures(measures, null);
        foreach (var telemetrySink in telemetrySinks)
        {
            telemetrySink.TrackEvent(name, gatheredDimensions, gatheredMeasures);
        }
    }

    /// <summary>
    /// Track an exception across all registered telemetry sinks
    /// </summary>
    public void TrackException(Exception exception, Dimensions dimensions = null, Measures measures = null)
    {
        if (exception == null) return;

        var gatheredDimensions = GatherDimensions(dimensions, null);
        var gatheredMeasures = GatherMeasures(measures, null);
        foreach (var telemetrySink in telemetrySinks)
        {
            telemetrySink.TrackException(exception, gatheredDimensions, gatheredMeasures);
        }
    }

    /// <summary>
    /// Track a trace message across all registered telemetry sinks
    /// </summary>
    public void TrackTrace(string message, SeverityLevel severityLevel, Dimensions dimensions = null)
    {
        var gatheredDimensions = GatherDimensions(dimensions, null);
        foreach (var telemetrySink in telemetrySinks)
        {
            telemetrySink.TrackTrace(message, severityLevel, gatheredDimensions);
        }
    }

    /// <summary>
    /// Gather the dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions.</param>
    /// <param name="exception">The exception.</param>
    private Dimensions GatherDimensions(Dimensions dimensions, Exception exception)
    {
        var dimensionPairs = (dimensions ?? []).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
        var exceptionPairs = (exception?.GetDimensions() ?? ImmutableDictionary<string, string>.Empty).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
        var scopePairs = observabilityScope.Dimensions.Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
        var systemContextPairs = systemContext.GetDimensions().Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
        // Reduce all input pairs into a dictionary. Take the last version of a value, which gives the system context and exceptions priority
        var pairs = scopePairs
            .Concat(dimensionPairs)
            .Concat(exceptionPairs)
            .Concat(systemContextPairs)
            .GroupBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
            .Select(kp => KeyValuePair.Create(kp.Key, (string)kp.Last().Value));
        return new Dimensions(pairs);
    }

    /// <summary>
    /// Gather the measures.
    /// </summary>
    /// <param name="measures">The measures.</param>
    /// <param name="exception">The exception.</param>
    private Measures GatherMeasures(Measures measures, Exception exception)
    {
        var dimensionPairs = (measures ?? []).Select(kp => new KeyValuePair<string, double>(kp.Key, kp.Value));
        var exceptionPairs = (exception?.GetMeasures() ?? ImmutableDictionary<string, double>.Empty).Select(kp => new KeyValuePair<string, double>(kp.Key, kp.Value));
        var scopePairs = observabilityScope.Measures.Select(kp => new KeyValuePair<string, double>(kp.Key, kp.Value));
        // Reduce all input pairs into a dictionary. Take the last version of a value, which gives exceptions priority
        var pairs = scopePairs
            .Concat(dimensionPairs)
            .Concat(exceptionPairs)
            .GroupBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
            .Select(kp => KeyValuePair.Create(kp.Key, kp.Last().Value));
        return new Measures(pairs);
    }
}
