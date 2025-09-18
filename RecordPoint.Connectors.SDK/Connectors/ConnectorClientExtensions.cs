using RecordPoint.Connectors.SDK.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Extension methods shared amongst all connector clients
    /// </summary>
    public static class ConnectorClientExtensions
    {
        /// <summary>
        /// Convert a connector config model into a connector data model suitable for the database
        /// </summary>
        /// <param name="connectorConfig">Notification model containing a connector notification</param>
        /// <returns></returns>
        public static ConnectorConfigurationModel ConvertToConnectorData(ConnectorConfigModel connectorConfig)
        {
            var data = JsonSerializer.Serialize(connectorConfig);
            var connectorData = new ConnectorConfigurationModel
            {
                ConnectorId = connectorConfig.Id,
                ConnectorTypeId = connectorConfig.ConnectorTypeId,
                DisplayName = connectorConfig.DisplayName,
                Status = connectorConfig.Status,
                TenantId = connectorConfig.TenantId,
                Data = data
            };
            return connectorData;
        }

        /// <summary>
        /// Convert a connector data model back into a connector config
        /// </summary>
        /// <param name="connectorData">Connector data to convert</param>
        /// <returns>Connector config</returns>
        public static ConnectorConfigModel ConvertToConnectorConfig(ConnectorConfigurationModel connectorData)
        {
            var connectorConfig = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorData.Data);
            return connectorConfig;
        }


        /// <summary>
        /// Set the connector asynchronously.
        /// </summary>
        /// <param name="connectorClient">The connector client.</param>
        /// <param name="connectorConfig">The connector config.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public static Task SetConnectorAsync(this IConnectorConfigurationManager connectorClient, ConnectorConfigModel connectorConfig, CancellationToken cancellationToken)
        {
            return connectorClient.SetConnectorConfigurationAsync(ConvertToConnectorData(connectorConfig), cancellationToken);
        }

        /// <summary>
        /// Get the connector asynchronously.
        /// </summary>
        /// <param name="connectorClient">The connector client.</param>
        /// <param name="connectorId">The connector id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ConnectorConfigModel>]]></returns>
        public static async Task<ConnectorConfigModel> GetConnectorAsync(this IConnectorConfigurationManager connectorClient, string connectorId, CancellationToken cancellationToken)
        {
            var connectorData = await connectorClient.GetConnectorConfigurationAsync(connectorId, cancellationToken);
            return connectorData != null ? ConvertToConnectorConfig(connectorData) : null;
        }

        /// <summary>
        /// List the connectors asynchronously.
        /// </summary>
        /// <param name="connectorClient">The connector client.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<List<ConnectorConfigModel>>]]></returns>
        public static async Task<List<ConnectorConfigModel>> ListConnectorsAsync(this IConnectorConfigurationManager connectorClient, CancellationToken cancellationToken)
        {
            var connectorConfigurations = await connectorClient.GetAllConnectorConfigurationsAsync(cancellationToken);
            return connectorConfigurations.Select(a => ConvertToConnectorConfig(a)).ToList();
        }
    }
}