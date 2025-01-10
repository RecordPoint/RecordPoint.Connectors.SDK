using System.Diagnostics;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Provides a simple wrapper to measure the timing of method calls.
    /// Surround any method call with a using statement which instantiates
    /// a PerformanceEvent and disposes of it once the method call has completed
    /// to use.
    /// </summary>
    public class PerformanceEvent : IPerformanceEvent
    {
        private readonly string _message;
        private readonly Stopwatch _stopWatch = new();
        private readonly ILog _log;
        private readonly Type _type;
        private readonly string _method;
        private Exception _ex;
        private bool disposedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="method">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event. In the
        /// event that an exception occurs, the exception message will be appended to the message.</param>
        /// <param name="log">An ILog instance which will do the actual recording</param>
        public PerformanceEvent(Type type, string method, string message, ILog log)
        {
            _message = message;
            _log = log;
            _type = type;
            _method = method;
            _stopWatch.Start();
        }

        /// <summary>
        /// Set the Exception property of the PerformanceEvent.
        /// Used to communicate any Exceptions when the trace is logged.
        /// Typically used in a finally or catch block so that the exception can be 
        /// traced out before being rethrown from the code wrapped by a PerformanceEvent
        /// </summary>
        /// <param name="ex"></param>
        public void Exception(Exception ex)
        {
            _ex = ex;
        }

        /// <summary>
        /// Logs to LogTimingVerbose on disposal of the PerformanceEvent.
        /// If an exception has been recorded on the PerformanceEvent, instead
        /// logs to LogTimingWarning after appending _Faulted to the method name
        /// and appending the exception to the message
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Stop existing stop watch
                    _stopWatch.Stop();

                    var message = _message;
                    var method = _method;

                    if (_ex != null)
                    {
                        method += "_Faulted";
                        message += " " + _ex.ToString();

                        // log timing event at the Warning level
                        _log?.LogWarning(_type, method, message, _stopWatch.Elapsed.Ticks);
                    }
                    else
                    {
                        // log timing event at the Verbose level
                        _log?.LogVerbose(_type, method, message, _stopWatch.Elapsed.Ticks);
                    }
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
