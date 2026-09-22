namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Represents configuration options for controlling logging behavior in the Connector.
/// </summary>
public class LoggingOptions
{
    /// <summary>
    /// Name for the configuration section
    /// </summary>
    public const string SECTION_NAME = "Logging";

    /// <summary>
    /// Determines if the Connector should log events related to the start of work, such as when a Channel Synchronisation begins.
    /// </summary>
    public bool LogStartWorkEvents { get; set; } = true;
}
