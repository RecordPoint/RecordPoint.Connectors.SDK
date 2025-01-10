using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health;

/// <summary>
/// Default Implementation for a HealthCheckReadyAction
/// </summary>
public class DefaultHealthCheckReadyAction : IHealthCheckReadyAction
{
    private readonly IHealthCheckManager _healthCheckManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="healthCheckManager"></param>
    public DefaultHealthCheckReadyAction(IHealthCheckManager healthCheckManager)
    {
        _healthCheckManager = healthCheckManager;
    }

    /// <inheritdoc/>
    public Task<bool> CheckIsReadyAsync()
    {
        static bool IsUptimeMeasure(HealthCheckMeasure measure) =>
            measure.HealthCheckType == UptimeStrategy.HEALTH_CHECK_TYPE &&
            measure.Name == UptimeStrategy.UPTIME_SECONDS_NAME &&
            measure.Value > 0;

        var isUp = _healthCheckManager.HealthCheckResult?.Measures?.Exists(IsUptimeMeasure) ?? false;
        return Task.FromResult(isUp);
    }
}
