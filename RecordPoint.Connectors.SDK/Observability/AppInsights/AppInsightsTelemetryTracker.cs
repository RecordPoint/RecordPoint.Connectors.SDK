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
    /// The app insights telemetry tracker.
    /// </summary>
    public class AppInsightsTelemetryTracker : ITelemetryTracker, IDisposable
    {
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IScopeManager _scopeManager;
        /// <summary>
        /// The feature toggle provider.
        /// </summary>
        private readonly IToggleProvider _featureToggleProvider;
        /// <summary>
        /// The application insight options.
        /// </summary>
        private readonly IOptions<ApplicationInsightOptions> _applicationInsightOptions;
        /// <summary>
        /// The system context.
        /// </summary>
        private readonly ISystemContext _systemContext;
        /// <summary>
        /// The telemetry client.
        /// </summary>
        private readonly Lazy<TelemetryClient> _telemetryClient;
        /// <summary>
        /// The disposed value.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Gets the telemetry submission.
        /// </summary>
        private string TelemetrySubmission => $"{_systemContext.GetConnectorName()}-TelemetrySubmission";

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsTelemetryTracker"/> class.
        /// </summary>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="featureToggleProvider">The feature toggle provider.</param>
        /// <param name="applicationInsightOptions">The application insight options.</param>
        /// <param name="systemContext">The system context.</param>
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

        /// <summary>
        /// Gets the instrumentation key.
        /// </summary>
        public string InstrumentationKey => _applicationInsightOptions.Value?.InstrumentationKey;


        /// <summary>
        /// Creates telemetry client.
        /// </summary>
        /// <exception cref="RequiredValueNullException"></exception>
        /// <returns>A TelemetryClient</returns>
        private TelemetryClient CreateTelemetryClient()
        {
            if (string.IsNullOrWhiteSpace(InstrumentationKey))
                throw new RequiredValueNullException(nameof(InstrumentationKey));
            var telemetryConfiguration = new TelemetryConfiguration(InstrumentationKey);
            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            return telemetryClient;
        }

        /// <summary>
        /// Checks if is configured.
        /// </summary>
        /// <returns>A bool</returns>
        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(InstrumentationKey);
        }

        /// <summary>
        /// Checks if is enabled.
        /// </summary>
        /// <returns>A bool</returns>
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

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dimensions">The dimensions.</param>
        /// <param name="measures">The measures.</param>
        public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
        {
            if (!IsEnabled())
                return;

            _telemetryClient.Value.TrackEvent(name, GatherDimensions(dimensions, null), measures);
        }

        /// <summary>
        /// Tracks the exception.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="dimensions">The dimensions.</param>
        /// <param name="measures">The measures.</param>
        public void TrackException(string name, Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            if (!IsEnabled())
                return;

            _telemetryClient.Value.TrackException(exception, GatherDimensions(dimensions, exception), measures);
        }

        /// <summary>
        /// Gather the dimensions.
        /// </summary>
        /// <param name="dimensions">The dimensions.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A Dimensions</returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void Flush()
        {
            if (!IsEnabled())
                return;
            if (_telemetryClient.IsValueCreated)
            {
                _telemetryClient.Value.Flush();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
