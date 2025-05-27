using Microsoft.ApplicationInsights;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights;

/// <summary>
/// Contract for a factory to create an instance of <see cref="TelemetryClient"/>.
/// </summary>
public interface ITelemetryClientFactory
{
    /// <summary>
    /// Creates an instance of <see cref="TelemetryClient"/> based on the configuration provided in <see cref="ApplicationInsightOptions"/>.
    /// </summary>
    /// <returns>A new instance of an Application Insights <see cref="TelemetryClient"></see>/</returns>
    public TelemetryClient GetTelemetryClient();
}
