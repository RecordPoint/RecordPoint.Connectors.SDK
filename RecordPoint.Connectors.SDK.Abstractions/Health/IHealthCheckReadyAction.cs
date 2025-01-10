namespace RecordPoint.Connectors.SDK.Health;

/// <summary>
/// Contract to implement a health check action for the ready endpoint.
/// </summary>
public interface IHealthCheckReadyAction
{
    /// <summary>
    /// Action to determine if the service instance is Ready.
    /// </summary>
    /// <returns></returns>
    Task<bool> CheckIsReadyAsync();
}
