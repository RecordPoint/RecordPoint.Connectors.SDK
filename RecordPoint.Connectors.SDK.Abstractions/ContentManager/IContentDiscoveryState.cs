namespace RecordPoint.Connectors.SDK.Abstractions.ContentManager;

/// <summary>
/// Contract for a State object that is used to store the state of a Content Discovery operation
/// </summary>
public interface IContentDiscoveryState
{
    /// <summary>
    /// The number of seconds the last execution was delayed by
    /// </summary>
    /// <remarks>
    /// Used for exponential backoff
    /// </remarks>
    int LastBackOffDelaySeconds { get; set; }
}
