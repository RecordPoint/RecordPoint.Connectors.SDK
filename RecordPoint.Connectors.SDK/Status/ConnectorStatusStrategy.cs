using RecordPoint.Connectors.SDK.Connectors;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// The connector status strategy.
    /// </summary>
    public class ConnectorStatusStrategy : IStatusStrategy
    {
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorStatusStrategy"/> class.
        /// </summary>
        /// <param name="connectorManager">The connector manager.</param>
        public ConnectorStatusStrategy(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        /// <summary>
        /// Get status text.
        /// </summary>
        /// <param name="connectorModel">The connector model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<List<string>>]]></returns>
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