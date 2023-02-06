using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Main front end for the health check component
    /// </summary>
    public class HealthCheckManager : IHealthCheckManager
    {

        private readonly IServiceProvider _serviceProvider;

        public HealthCheckManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunHealthCheckAsync(CancellationToken cancellationToken)
        {
            var healthCheckOperation = _serviceProvider.GetRequiredService<HealthCheckOperation>();
            await healthCheckOperation.RunAsync(null, cancellationToken);
        }
    }
}
