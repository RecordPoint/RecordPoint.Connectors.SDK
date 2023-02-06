using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// Standard health checker that invokes all registered health check providers
    /// </summary>
    public class HealthCheckService : BackgroundService
    {

        private readonly ISystemContext _systemContext;
        private readonly IScopeManager _scopeManager;
        private readonly IHealthCheckManager _healthCheckManager;
        private readonly IOptions<HealthCheckOptions> _healthCheckOptions;

        private readonly string _serviceID;

        private const string SERVICE_TYPE = "Health Checker";

        private TimeSpan StartDelay => TimeSpan.FromSeconds(_healthCheckOptions.Value.HealthCheckStartDelaySeconds);

        private TimeSpan CheckDelay => TimeSpan.FromSeconds(_healthCheckOptions.Value.HealthCheckFrequencySeconds);

        public HealthCheckService(
            IHealthCheckManager healthCheckManager,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            IOptions<HealthCheckOptions> healthCheckOptions)
        {
            _systemContext = systemContext;
            _scopeManager = scopeManager;
            _healthCheckManager = healthCheckManager;
            _healthCheckOptions = healthCheckOptions;

            _serviceID = Guid.NewGuid().ToString();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(StartDelay, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
                using var serviceScope = _scopeManager.BeginServiceScope(SERVICE_TYPE, _serviceID);
                await _healthCheckManager.RunHealthCheckAsync(stoppingToken);
                await Task.Delay(CheckDelay, stoppingToken);
            }
        }
    }
}
