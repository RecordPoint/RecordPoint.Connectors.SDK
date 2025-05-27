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

        telemetryClientFactory.GetTelemetryClient()
            .TrackEvent(name, dimensions, measures);
    }

    /// <summary>
    /// Tracks an exception
    /// </summary>
    public void TrackException(Exception exception, Dimensions dimensions = null, Measures measures = null)
    {
        if (!IsEnabled())
            return;

        telemetryClientFactory.GetTelemetryClient()
            .TrackException(exception, dimensions, measures);
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
    /// Checks if is configured.
    /// </summary>
    /// <returns>A bool</returns>
    private bool IsConfigured()
    {
        return !string.IsNullOrEmpty(applicationInsightOptions.Value.ConnectionString)
            || !string.IsNullOrEmpty(applicationInsightOptions.Value.InstrumentationKey);
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
