namespace RecordPoint.Connectors.SDK.Observability
{
    /// <summary>
    /// Observability event types
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Service startup
        /// </summary>
        Startup,
        /// <summary>
        /// Service shutdown
        /// </summary>
        Shutdown,
        /// <summary>
        /// Unit of work started
        /// </summary>
        Start,
        /// <summary>
        /// Unit of work finished
        /// </summary>
        Finish,
        /// <summary>
        /// Major decision completed
        /// </summary>
        Decision
    }

}
