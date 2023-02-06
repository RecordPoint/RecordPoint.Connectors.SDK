namespace RecordPoint.Connectors.SDK.Observability
{
    /// <summary>
    /// Observability event types
    /// </summary>
    public enum EventType
    {
        Startup, // Service startup
        Shutdown, // Service shutdown
        Start, // Unit of work started
        Finish, // Unit of work finished
        Decision, // Major decision completed
    }

}
