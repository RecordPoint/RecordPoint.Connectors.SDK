using RecordPoint.Connectors.SDK.Client.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <param name="notificationModel">Notification model containing a connector notification</param>
        /// <returns></returns>
        private static ConnectorConfigurationModel ConvertToConnectorData(ConnectorConfigModel connectorConfig)
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
        private static ConnectorConfigModel ConvertToConnectorConfig(ConnectorConfigurationModel connectorData)
        {
            var connectorConfig = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorData.Data);
            return connectorConfig;
        }


        public static Task SetConnectorAsync(this IConnectorConfigurationManager connectorClient, ConnectorConfigModel connectorConfig, CancellationToken cancellationToken)
        {
            return connectorClient.SetConnectorConfigurationAsync(ConvertToConnectorData(connectorConfig), cancellationToken);
        }

        public static async Task<ConnectorConfigModel> GetConnectorAsync(this IConnectorConfigurationManager connectorClient, string connectorId, CancellationToken cancellationToken)
        {
            var connectorData = await connectorClient.GetConnectorConfigurationAsync(connectorId, cancellationToken);
            return connectorData != null ? ConvertToConnectorConfig(connectorData) : null;
        }

        public static async Task<List<ConnectorConfigModel>> ListConnectorsAsync(this IConnectorConfigurationManager connectorClient, CancellationToken cancellationToken)
        {
            var connectorConfiurations = await connectorClient.GetAllConnectorConfigurationsAsync(cancellationToken);
            return connectorConfiurations.Select(a => ConvertToConnectorConfig(a)).ToList();
        }
    }
}