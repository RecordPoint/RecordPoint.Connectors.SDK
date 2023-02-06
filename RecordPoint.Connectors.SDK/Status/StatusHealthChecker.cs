using RecordPoint.Connectors.SDK.Health;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// Health checker that just adds the status as health check dimensions
    /// </summary>
    public class StatusHealthChecker : IHealthCheckStrategy
    {
        public const string STATUS_HEALTH_CHECK_TYPE = "Status";

        private readonly IStatusManager _statusManager;

        public string HealthCheckType => STATUS_HEALTH_CHECK_TYPE;

        public StatusHealthChecker(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        public async Task<List<HealthCheckItem>> HealthCheckAsync(CancellationToken stoppingToken)
        {
            var connectorConfigurationStatuses = await _statusManager.GetStatusModelAsync(stoppingToken);

            var healthCheckItems = connectorConfigurationStatuses
                .SelectMany(a => a.Status, (connectorStatus, statusText) => new { connectorStatus.ConnectorId, StatusText = statusText.Split('|') })
                .Where(a => a.StatusText.Length == 2)
                .Select(a => new HealthCheckDimension() { HealthCheckType = STATUS_HEALTH_CHECK_TYPE, ConnectorId = a.ConnectorId, Name = a.StatusText[0], Value = a.StatusText[1] })
                .OfType<HealthCheckItem>()
                .ToList();

            return healthCheckItems;
        }
    }
}
