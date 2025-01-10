namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Represents a single performance monitored event.
    /// </summary>
    public interface IPerformanceEvent : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        void Exception(Exception ex);
    }
}
