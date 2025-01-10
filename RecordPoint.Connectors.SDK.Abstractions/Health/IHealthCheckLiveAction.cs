namespace RecordPoint.Connectors.SDK.Health;

/// <summary>
/// Contract to implement a health check action for the live endpoint.
/// </summary>
public interface IHealthCheckLiveAction
{
    /// <summary>
    /// Action to determine if the service instance is Live.
    /// </summary>
    /// <returns></returns>
    Task<bool> CheckIsLiveAsync();
}
