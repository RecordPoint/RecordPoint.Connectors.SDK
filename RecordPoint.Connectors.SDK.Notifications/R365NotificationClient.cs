using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Secrets;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// R365 Standard Client
    /// </summary>
    public class R365NotificationClient : IR365NotificationClient
    {

        private readonly IR365ConfigurationClient _r365ConfigurationClient;
        private readonly IConnectorConfigurationManager _connectorConfigurationManager;
        private readonly IScopeManager _scopeManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r365ConfigurationClient"></param>
        /// <param name="scopeManager"></param>
        /// <param name="connectorConfigurationManager"></param>
        public R365NotificationClient(
            IR365ConfigurationClient r365ConfigurationClient,
            IScopeManager scopeManager,
            IConnectorConfigurationManager connectorConfigurationManager)
        {
            _r365ConfigurationClient = r365ConfigurationClient;
            _scopeManager = scopeManager;
            _connectorConfigurationManager = connectorConfigurationManager;
        }

        private static Dimensions GetDimensions()
        {
            var dimensions = new Dimensions()
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Records365.ToString(),
            };
            return dimensions;
        }

        /// <summary>
        /// Ensure configuration is loaded
        /// </summary>
        private R365ConfigurationModel LoadConfiguration()
        {
            return _r365ConfigurationClient.GetR365Configuration();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConfigured()
        {
            var r365Configuration = LoadConfiguration();
            return r365Configuration != null;
        }

        private static ApiClientFactorySettings GetApiClientFactorySettings(R365ConfigurationModel r365Configuration)
        {
            var settings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = r365Configuration.ConnectorApiUrl
            };
            return settings;
        }

        private static AuthenticationHelperSettings GetAuthenticationHelperSettings(R365ConfigurationModel r365Configuration, string tenantDomainName)
        {
            var clientSecret = EncryptionExtensions.GetSecureSecret(r365Configuration.ClientSecret);
            var settings = new AuthenticationHelperSettings
            {
                ClientId = r365Configuration.ClientId.ToString(),
                ClientSecret = clientSecret,
                TenantDomainName = tenantDomainName,
                AuthenticationResource = r365Configuration.Audience
            };
            return settings;
        }

        /// <summary>
        /// Returns all the pull notifications for a tenant
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List<ConnectorNotificationModel/>></returns>
        public async Task<List<ConnectorNotificationModel>> GetAllPendingNotifications(CancellationToken cancellationToken)
        {
            var r365Configuration = LoadConfiguration();
            var connectorConfigs = await _connectorConfigurationManager.ListConnectorsAsync(cancellationToken);
            var notifications = new List<ConnectorNotificationModel>();
            var pullManager = new NotificationPullManager();
            foreach (var connector in connectorConfigs)
            {
                try
                {
                    var authenticationHelperSettings =
                        GetAuthenticationHelperSettings(r365Configuration, connector.TenantDomainName);
                    var apiClientFactorySettings = GetApiClientFactorySettings(r365Configuration);
                    var connectorNotifications = await pullManager.GetAllPendingConnectorNotifications(
                        apiClientFactorySettings, authenticationHelperSettings,
                        connector.Id, cancellationToken);
                    notifications.AddRange(connectorNotifications);
                }
                catch (Exception ex)
                {
                    _scopeManager.Invoke(GetDimensions(), () =>
                    {
                        _scopeManager.Logger.LogError(ex, "Error getting notifications for connector {ConnectorId}",
                            connector.Id);
                    });
                }
            }
            return notifications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AcknowledgeNotificationAsync(ConnectorNotificationModel notification, ProcessingResult result, string message, CancellationToken cancellationToken)
        {
            await _scopeManager.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration();
                    var pullManager = new NotificationPullManager();
                    await pullManager.AcknowledgeNotification(
                        GetApiClientFactorySettings(r365Configuration),
                        GetAuthenticationHelperSettings(r365Configuration, notification.ConnectorConfig.TenantDomainName),
                        notification.ToAcknowledge(result, message),
                        cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
        }
    }
}
