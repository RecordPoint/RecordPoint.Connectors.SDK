using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionResultBase
    {
        /// <summary>
        /// When the action requests a backoff due to Content Source throttling, this specifies how the semphore should lock
        /// other requests to the content source
        /// </summary>
        public SemaphoreLockType? SemaphoreLockType { get; set; }

        /// <summary>
        /// The number of seconds to delay the next execution of the Channel Discovery
        /// Also specifies the number of seconds to set a semaphore lock when a BackOff result is returned
        /// </summary>
        /// <remarks>When null, the operation will use the default delay configuration</remarks>
        public int? NextDelay { get; set; } = null;

        /// <summary>
        /// Observability Dimensions
        /// </summary>
        public Dimensions Dimensions { get; set; } = [];

        /// <summary>
        /// Observability Measures
        /// </summary>
        public Measures Measures { get; set; } = [];
    }
}
