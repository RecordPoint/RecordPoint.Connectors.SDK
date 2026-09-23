using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using System.Security.Cryptography;
using System.Text;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Handler for a connector secret notification
    /// </summary>
    public class ConnectorSecretHandler : NotificationHandler
    {
        /// <summary>
        /// Connector Secret Notification Type
        /// </summary>
        public const string CONNECTOR_SECRET_NOTIFICATION_TYPE = "ConnectorSecret";

        private readonly IConnectorConfigurationManager _connectorManager;
        private readonly IConnectorSecretAction _connectorSecretAction;
        private readonly IConnectorSecretDecryptor _secretDecryptor;

        /// <summary>
        /// Handler for a connector secret notification
        /// </summary>
        /// <param name="connectorManager"></param>
        /// <param name="connectorSecretAction"></param>
        /// <param name="secretDecryptor"></param>
        public ConnectorSecretHandler(IConnectorConfigurationManager connectorManager, IConnectorSecretAction connectorSecretAction, IConnectorSecretDecryptor secretDecryptor)
        {
            _connectorManager = connectorManager;
            _connectorSecretAction = connectorSecretAction;
            _secretDecryptor = secretDecryptor;
        }

        /// <summary>
        /// Connector Secret Notification Type
        /// </summary>
        public override string NotificationType => CONNECTOR_SECRET_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the Connector Secret Notification
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            var connectorSecrets = notification.Context.ContextToList<ConnectorSecret>();

            // Shared with the connector request path, so the two cannot drift.
            _secretDecryptor.DecryptInPlace(connectorSecrets, notification.ConnectorConfig);

            var connectorConfiguration = await _connectorManager.GetConnectorAsync(notification.ConnectorId, cancellationToken);

            await _connectorSecretAction.SaveSecretsAsync(connectorConfiguration, connectorSecrets);

            return NotificationOutcome.OK();
        }

    }
}
