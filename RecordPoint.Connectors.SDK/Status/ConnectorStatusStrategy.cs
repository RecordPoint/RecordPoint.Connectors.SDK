using RecordPoint.Connectors.SDK.Connectors;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Status
{
    public class ConnectorStatusStrategy : IStatusStrategy
    {
        private readonly IConnectorConfigurationManager _connectorManager;

        public ConnectorStatusStrategy(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        public async Task<List<string>> GetStatusText(ConnectorConfigurationModel connectorModel, CancellationToken cancellationToken)
        {
            var connectorStatus = await _connectorManager.GetConnectorStatusAsync(connectorModel.ConnectorId, cancellationToken);
            var binarySubmissionStatus = await _connectorManager.GetBinarySubmissionStatusAsync(connectorModel.ConnectorId, cancellationToken);
            var result = new List<string>
            {
                $"ConnectorStatus|{connectorStatus.Enabled}",
                $"ConnectorStatusReason|{connectorStatus.EnabledReason}",
                $"ConnectorId|{connectorModel.ConnectorId}",
                $"BinarySubmissionStatus|{binarySubmissionStatus.Enabled}",
                $"BinarySubmissionStatusReason|{binarySubmissionStatus.EnabledReason}"
            };

            return result;
        }
    }
}