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
    /// The health check service.
    /// </summary>
    public class HealthCheckService : BackgroundService
    {

        /// <summary>
        /// The system context.
        /// </summary>
        private readonly ISystemContext _systemContext;
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IScopeManager _scopeManager;
        /// <summary>
        /// The health check manager.
        /// </summary>
        private readonly IHealthCheckManager _healthCheckManager;
        /// <summary>
        /// The health check options.
        /// </summary>
        private readonly IOptions<HealthCheckOptions> _healthCheckOptions;

        /// <summary>
        /// The service ID.
        /// </summary>
        private readonly string _serviceID;

        /// <summary>
        /// The SERVICE TYPE.
        /// </summary>
        private const string SERVICE_TYPE = "Health Checker";

        /// <summary>
        /// Gets the start delay.
        /// </summary>
        private TimeSpan StartDelay => TimeSpan.FromSeconds(_healthCheckOptions.Value.HealthCheckStartDelaySeconds);

        /// <summary>
        /// Gets the check delay.
        /// </summary>
        private TimeSpan CheckDelay => TimeSpan.FromSeconds(_healthCheckOptions.Value.HealthCheckFrequencySeconds);

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckService"/> class.
        /// </summary>
        /// <param name="healthCheckManager">The health check manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="healthCheckOptions">The health check options.</param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
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
