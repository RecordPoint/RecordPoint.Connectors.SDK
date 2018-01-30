using System;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Provides a simple intrusive means of measuring how long it takes to execute a certain code path.
    /// </summary>
    public interface IPerformance
    {
        ILog Log { get;  set; }

        /// <summary>
        /// Creates a performance event and starts a timer. Dispose the returned interface to stop the timer. The time between
        /// CreateEvent and Dispose will be logged to the ILog instance on this object.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        IPerformanceEvent CreateEvent(Type type, string methodName, string message);
    }
}
