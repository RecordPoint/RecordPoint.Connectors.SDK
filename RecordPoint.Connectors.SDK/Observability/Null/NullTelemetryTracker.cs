using System;

namespace RecordPoint.Connectors.SDK.Observability.Null
{

    /// <summary>
    /// Telemetry tracker that does nothing. Useful for initial development, testing.
    /// </summary>
    public class NullTelemetryTracker : ITelemetryTracker
    {
        public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }

        public void TrackException(string name, Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }
    }
}
