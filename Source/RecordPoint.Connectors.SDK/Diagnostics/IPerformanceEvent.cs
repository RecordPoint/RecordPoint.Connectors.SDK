using System;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Represents a single performance monitored event.
    /// </summary>
    public interface IPerformanceEvent : IDisposable
    {
        void Exception(Exception ex);
    }
}
