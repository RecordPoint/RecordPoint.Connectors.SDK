using RecordPoint.Connectors.SDK.Context;
using System;

namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Extension methods for the ITelemetryTracker interface
/// </summary>
public static class ITelemetryTrackerExtensions
{
    /// <summary>
    /// Returns an observability scope containing default dimensions from the system context
    /// </summary>
    /// <param name="telemetryTracker">The Telemetry Tracker</param>
    /// <param name="systemContext">The current system context</param>
    /// <param name="dimensions"></param>
    /// <param name="measures"></param>
    /// <returns></returns>
    public static IDisposable CreateRootServiceScope(this ITelemetryTracker telemetryTracker, ISystemContext systemContext, Dimensions dimensions = null, Measures measures = null)
    {
        var scopeDimensions = new Dimensions
        {
            { StandardDimensions.COMPANY, systemContext.GetCompanyName() },
            { StandardDimensions.SYSTEM, systemContext.GetConnectorName() },
            { StandardDimensions.SERVICE, systemContext.GetServiceName() }
        };

        foreach (var dimension in dimensions ?? [])
        {
            if (scopeDimensions.ContainsKey(dimension.Key))
                continue;
            scopeDimensions.Add(dimension.Key, dimension.Value);
        }

        return telemetryTracker.BeginScope(scopeDimensions, measures);
    }

}