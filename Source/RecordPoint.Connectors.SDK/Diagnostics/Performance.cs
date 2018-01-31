using System;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    public class Performance : IPerformance
    {
        public ILog Log { get; set; } = null;

        public IPerformanceEvent CreateEvent(Type type, string methodName, string message)
        {
            return new PerformanceEvent(type, methodName, message, Log);
        }
    }
}
