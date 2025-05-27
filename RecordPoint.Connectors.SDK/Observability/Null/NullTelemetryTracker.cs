using System;

namespace RecordPoint.Connectors.SDK.Observability.Null
{
    /// <summary>
    /// Telemetry tracker that does nothing. Useful for initial development, testing.
    /// </summary>
    public class NullTelemetryTracker : ITelemetryTracker
    {
        /// <summary>
        /// Begins an observability scope
        /// </summary>
        public IDisposable BeginScope(Dimensions dimensions = null, Measures measures = null)
        {
            return new NullObservabilityScope();
        }

        /// <summary>
        /// Track an Event
        /// </summary>
        public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }

        /// <summary>
        /// Track an Exception
        /// </summary>
        public void TrackException(Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }

        /// <summary>
        /// Track a Trace Message
        /// </summary>
        public void TrackTrace(string message, SeverityLevel severityLevel, Dimensions dimensions = null)
        {
            // Does nothing on purpose
        }
    }
}
