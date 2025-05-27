using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights;

/// <summary>
/// Factory to create an instance of <see cref="TelemetryClient"/> based on the configuration provided in <see cref="ApplicationInsightOptions"/>.
/// </summary>
/// <param name="applicationInsightOptions">Configuration options for the <see cref="TelemetryClient"></see>/</param>
public class TelemetryClientFactory(IOptions<ApplicationInsightOptions> applicationInsightOptions) : ITelemetryClientFactory
{
    private TelemetryClient _telemetryClient = null;

    /// <summary>
    /// Creates an instance of <see cref="TelemetryClient"/> based on the configuration provided in <see cref="ApplicationInsightOptions"/>.
    /// </summary>
    /// <returns>A new instance of an Application Insights <see cref="TelemetryClient"></see>/</returns>
    /// <exception cref="RequiredValueNullException"></exception>
    public TelemetryClient GetTelemetryClient()
    {
        if (_telemetryClient != null)
            return _telemetryClient;

        var connectionString = applicationInsightOptions.Value.ConnectionString;
#pragma warning disable CS0618 // Type or member is obsolete
        var instrumentationKey = applicationInsightOptions.Value.InstrumentationKey;

        if (string.IsNullOrWhiteSpace(connectionString) && string.IsNullOrWhiteSpace(instrumentationKey))
            throw new RequiredValueNullException(nameof(applicationInsightOptions.Value.ConnectionString));

        _telemetryClient = string.IsNullOrEmpty(connectionString)
            ? new TelemetryClient(new TelemetryConfiguration(instrumentationKey)) //Obsolete
            : new TelemetryClient(new TelemetryConfiguration
            {
                ConnectionString = connectionString
            });
#pragma warning restore CS0618 // Type or member is obsolete

        return _telemetryClient;
    }
}
