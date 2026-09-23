using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Toggles;
using System;
using AISeverityLevel = Microsoft.ApplicationInsights.DataContracts.SeverityLevel;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights;

/// <summary>
/// Application Insights Telemetry Sink
/// </summary>
public class ApplicationInsightsTelemetrySink(
    ITelemetryClientFactory telemetryClientFactory,
    IOptions<ApplicationInsightOptions> applicationInsightOptions,
    IToggleProvider featureToggleProvider,
    ISystemContext systemContext) : ITelemetrySink
{
    private string TelemetrySubmissionFeatureToggle => $"{systemContext.GetConnectorName()}-TelemetrySubmission";

    /// <summary>
    /// Tracks a custom event
    /// </summary>
    public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
    {
        if (!IsEnabled())
            return;

        var telemetry = new EventTelemetry(name);
        if (dimensions != null)
            foreach (var kv in dimensions)
                telemetry.Properties[kv.Key] = kv.Value;

        if (measures != null)
            foreach (var kv in measures)
                telemetry.Properties[kv.Key] = kv.Value.ToString();

        // Note: EventTelemetry.Metrics was removed in ApplicationInsights 3.x; measures are not tracked on events.
        telemetryClientFactory.GetTelemetryClient().TrackEvent(telemetry);
    }

    /// <summary>
    /// Tracks an exception
    /// </summary>
    public void TrackException(Exception exception, Dimensions dimensions = null, Measures measures = null)
    {
        if (!IsEnabled())
            return;

        var telemetry = new ExceptionTelemetry(exception);
        if (dimensions != null)
            foreach (var kv in dimensions)
                telemetry.Properties[kv.Key] = kv.Value;

        if (measures != null)
            foreach (var kv in measures)
                telemetry.Properties[kv.Key] = kv.Value.ToString();

        // Note: ExceptionTelemetry.Metrics was removed in ApplicationInsights 3.x; measures are not tracked on exceptions.
        telemetryClientFactory.GetTelemetryClient().TrackException(telemetry);
    }

    /// <summary>
    /// Tracks a trace message
    /// </summary>
    public void TrackTrace(string message, SeverityLevel severityLevel, Dimensions dimensions = null)
    {
        if ((int)applicationInsightOptions.Value.LogLevel > (int)severityLevel)
            return;

        if (!IsEnabled())
            return;

        var aiSeverityLevel = severityLevel switch
        {
            SeverityLevel.Verbose => AISeverityLevel.Verbose,
            SeverityLevel.Information => AISeverityLevel.Information,
            SeverityLevel.Warning => AISeverityLevel.Warning,
            SeverityLevel.Error => AISeverityLevel.Error,
            SeverityLevel.Critical => AISeverityLevel.Critical,
            _ => AISeverityLevel.Information
        };

        telemetryClientFactory.GetTelemetryClient()
            .TrackTrace(message, aiSeverityLevel, dimensions);
    }

    /// <summary>
    /// Tracks a metric value. Each call emits one <see cref="MetricTelemetry"/> item immediately.
    /// Note: the GetMetric / local-aggregation API was removed in ApplicationInsights 3.x.
    /// </summary>
    public void TrackMetric(string name, double value, Dimensions dimensions = null)
    {
        if (!IsEnabled())
            return;

        telemetryClientFactory.GetTelemetryClient().TrackMetric(name, value, dimensions);
    }

    /// <summary>
    /// Checks if is configured.
    /// </summary>
    /// <returns>A bool</returns>
    private bool IsConfigured()
    {
        return !string.IsNullOrEmpty(applicationInsightOptions.Value.ConnectionString);
    }

    /// <summary>
    /// Checks if is enabled.
    /// </summary>
    /// <returns>A bool</returns>
    private bool IsEnabled()
    {
        if (!IsConfigured())
            return false;

        var isToggled = featureToggleProvider
            .GetToggleBool(TelemetrySubmissionFeatureToggle, true);

        return isToggled && telemetryClientFactory
            .GetTelemetryClient()
            .IsEnabled();
    }

}
