using System;

namespace RecordPoint.Connectors.Diagnostics
{
    /// <summary>
    /// Represents a single performance monitored event.
    /// </summary>
    public interface IPerformanceEvent : IDisposable
    {
        void Exception(Exception ex);
    }
}
