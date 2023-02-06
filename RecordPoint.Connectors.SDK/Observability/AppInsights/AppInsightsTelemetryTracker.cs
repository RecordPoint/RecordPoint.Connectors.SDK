using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Toggles;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights
{

    /// <summary>
    /// Telemetry tracker that just records all events in application insights
    /// </summary>
    public class AppInsightsTelemetryTracker : ITelemetryTracker, IDisposable
    {
        private readonly IScopeManager _scopeManager;
        private readonly IToggleProvider _featureToggleProvider;
        private readonly IOptions<ApplicationInsightOptions> _applicationInsightOptions;
        private readonly ISystemContext _systemContext;
        private readonly Lazy<TelemetryClient> _telemetryClient;
        private bool disposedValue;

        private string TelemetrySubmission => $"{_systemContext.GetConnectorName()}-TelemetrySubmission";

        public AppInsightsTelemetryTracker(IScopeManager scopeManager,
                IToggleProvider featureToggleProvider, IOptions<ApplicationInsightOptions> applicationInsightOptions,
                ISystemContext systemContext)
        {
            _scopeManager = scopeManager;
            _featureToggleProvider = featureToggleProvider;
            _applicationInsightOptions = applicationInsightOptions;
            _telemetryClient = new Lazy<TelemetryClient>(CreateTelemetryClient);
            _systemContext = systemContext;
        }

        public string InstrumentationKey => _applicationInsightOptions.Value?.InstrumentationKey;


        private TelemetryClient CreateTelemetryClient()
        {
            if (string.IsNullOrWhiteSpace(InstrumentationKey))
                throw new RequiredValueNullException(nameof(InstrumentationKey));
            var telemetryConfiguration = new TelemetryConfiguration(InstrumentationKey);
            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            return telemetryClient;
        }

        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(InstrumentationKey);
        }

        public bool IsEnabled()
        {
            if (!IsConfigured())
                return false;
            // TODO: To support telemetry per tenant base .
            var toggleProviderTelemetryToggleOptions =
                _featureToggleProvider.GetToggleBool(TelemetrySubmission, true);

            var telemetryClient = _telemetryClient.Value;
            return toggleProviderTelemetryToggleOptions && telemetryClient.IsEnabled();
        }

        public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
        {
            if (!IsEnabled())
                return;

            _telemetryClient.Value.TrackEvent(name, GatherDimensions(dimensions, null), measures);
        }

        public void TrackException(string name, Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            if (!IsEnabled())
                return;

            _telemetryClient.Value.TrackException(exception, GatherDimensions(dimensions, exception), measures);
        }

        private Dimensions GatherDimensions(Dimensions dimensions, Exception exception)
        {
            var dimensionPairs = (dimensions ?? new Dimensions()).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            var exceptionPairs = (exception?.GetDimensions() ?? ImmutableDictionary<string, string>.Empty).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            var scopePairs = _scopeManager.Dimensions.Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            // Reduce all input pairs into a dictionary. Take the last version of a value, which gives exceptions priority
            var pairs =
                scopePairs
                .Concat(dimensionPairs)
                .Concat(exceptionPairs)
                .GroupBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
                .Select(kp => KeyValuePair.Create(kp.Key, (string)kp.Last().Value));
            return new Dimensions(pairs);
        }

        public void Flush()
        {
            if (!IsEnabled())
                return;
            if (_telemetryClient.IsValueCreated)
            {
                _telemetryClient.Value.Flush();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _telemetryClient.IsValueCreated)
                {
                    _telemetryClient.Value.Flush();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
