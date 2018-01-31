using System;
using System.Diagnostics;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    internal class PerformanceEvent : IPerformanceEvent
    {
        private readonly string _message;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly ILog _log = null;
        private readonly Type _type;
        private readonly string _method;
        private Exception _ex;

        public PerformanceEvent(Type type, string method, string message, ILog log)
        {
            _message = message;
            _log = log;
            _type = type;
            _method = method;
            _stopWatch.Start();
        }

        public void Exception(Exception ex)
        {
            _ex = ex;
        }

        public void Dispose()
        {
            // stop existing stop watch
            _stopWatch.Stop();
            
            var message = _message;
            var method = _method;

            if (_ex != null)
            {
                method += "_Faulted";
                message += " " + _ex.ToString();
            }

            // log timing event in ticks
            _log?.LogTimingEventTicks(_type, method, _stopWatch.Elapsed.Ticks, message);
        }
    }
}
