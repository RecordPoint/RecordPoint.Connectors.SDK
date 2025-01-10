using System;

namespace RecordPoint.Connectors.SDK.Observability.Null
{

    /// <summary>
    /// Telemetry tracker that does nothing. Useful for initial development, testing.
    /// </summary>
    public class NullTelemetryTracker : ITelemetryTracker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dimensions"></param>
        /// <param name="measures"></param>
        public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exception"></param>
        /// <param name="dimensions"></param>
        /// <param name="measures"></param>
        public void TrackException(string name, Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            // Does nothing on purpose
        }
    }
}
