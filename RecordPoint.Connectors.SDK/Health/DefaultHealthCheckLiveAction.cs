using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health;

/// <summary>
/// Default Implementation for a HealthCheckLiveAction
/// </summary>
public class DefaultHealthCheckLiveAction : IHealthCheckLiveAction
{
    private readonly IHealthCheckManager _healthCheckManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="healthCheckManager"></param>
    public DefaultHealthCheckLiveAction(IHealthCheckManager healthCheckManager)
    {
        _healthCheckManager = healthCheckManager;
    }

    /// <inheritdoc/>
    public Task<bool> CheckIsLiveAsync()
    {
        var isUp = _healthCheckManager.HealthCheckResult.Measures.Exists(a => a.HealthCheckType == UptimeStrategy.HEALTH_CHECK_TYPE && a.Name == UptimeStrategy.UPTIME_SECONDS_NAME && a.Value > 0);
        return Task.FromResult(isUp);
    }
}
