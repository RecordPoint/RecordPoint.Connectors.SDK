using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Status
{
    public class StatusManager : IStatusManager
    {
        private readonly IEnumerable<IStatusStrategy> _strategies;
        private readonly Connectors.IConnectorConfigurationManager _connectorManager;

        public StatusManager(IEnumerable<IStatusStrategy> strategies,
            Connectors.IConnectorConfigurationManager connectorManager)
        {
            _strategies = strategies;
            _connectorManager = connectorManager;

        }

        public async Task<List<StatusModel>> GetStatusModelAsync(CancellationToken cancellationToken)
        {
            var model = new List<StatusModel>();
            var connectorConfigurations = await _connectorManager.GetAllConnectorConfigurationsAsync(cancellationToken);

            if (connectorConfigurations.Any())
            {
                foreach (var connectorConfiguration in connectorConfigurations)
                {
                    var connectorStatus = new StatusModel
                    {
                        ConnectorId = connectorConfiguration.ConnectorId,
                        Status = new()
                    };
                    model.Add(connectorStatus);
                    foreach (var strategy in _strategies)
                    {
                        connectorStatus.Status = connectorStatus.Status.Concat(await strategy.GetStatusText(connectorConfiguration, cancellationToken)).ToList();
                    }
                }
                return model;
            }
            else
            {
                return model;
            }

        }
    }
}
