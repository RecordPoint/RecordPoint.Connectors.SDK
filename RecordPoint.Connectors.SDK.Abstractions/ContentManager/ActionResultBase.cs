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
        /// Overrides the maximum backoff delay applied to this result, in seconds.
        /// </summary>
        /// <remarks>
        /// When null, QueueableWorkBase.DEFAULT_MAX_BACKOFF_DELAY_SECONDS (one hour) applies.
        /// Set this only when the content source is genuinely slow to respond and a long wait is expected —
        /// for example a disposal that completes weeks later — not to work around throttling, where the
        /// default hour is the right ceiling. The value is clamped to
        /// QueueableWorkBase.ABSOLUTE_MAX_BACKOFF_DELAY_SECONDS.
        /// Because a BackOff also holds a semaphore lock for this long, raise it only for locks that are
        /// scoped narrowly enough that other work is not held up.
        /// </remarks>
        public int? MaxNextDelay { get; set; } = null;

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
