using RecordPoint.Connectors.SDK.Caching.Semaphore;

namespace RecordPoint.Connectors.SDK.ContentManager
{
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
    }
}
