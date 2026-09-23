using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Secrets;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// R365 standard Notifications client.
    /// </summary>
    public class R365NotificationClient : IR365NotificationClient
    {
        private readonly IR365ConfigurationClient _r365ConfigurationClient;
        private readonly IObservabilityScope _observabilityScope;
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly NotificationsPollerOptions _notificationsPollerOptions;
        private readonly INotificationApiManager _notificationPullManager;

        /// <summary>
        /// 
        /// </summary>
        public R365NotificationClient(
            IR365ConfigurationClient r365ConfigurationClient,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IOptions<NotificationsPollerOptions> notificationsPollerOptions,
            INotificationApiManager notificationPullManager)
        {
            _r365ConfigurationClient = r365ConfigurationClient;
            _observabilityScope = observabilityScope;
            _telemetryTracker = telemetryTracker;
            _notificationsPollerOptions = notificationsPollerOptions?.Value ?? throw new ArgumentNullException(nameof(notificationsPollerOptions));
            _notificationPullManager = notificationPullManager;
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
        private R365ConfigurationModel LoadConfiguration(string key = "")
        {
            return _r365ConfigurationClient.GetR365Configuration(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConfigured()
        {
            return _r365ConfigurationClient.R365ConfigurationExists();
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
        /// Returns all the pull notifications for a configured connector types for a tenant
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List<ConnectorNotificationModel/>></returns>
        public async Task<List<ConnectorNotificationModel>> GetAllPendingNotifications(CancellationToken cancellationToken)
        {
            var deployableConnectorTypes = _notificationsPollerOptions.ConnectorTypes;
            var notifications = new List<ConnectorNotificationModel>();

            if (deployableConnectorTypes == null || deployableConnectorTypes.Length == 0)
            {
                return notifications;
            }

            foreach (var connectorType in deployableConnectorTypes)
            {
                try
                {
                    // Load default configuration
                    var r365Configuration = LoadConfiguration();
                    var authenticationHelperSettings =
                        GetAuthenticationHelperSettings(r365Configuration, _notificationsPollerOptions.TenantDomainName);
                    var apiClientFactorySettings = GetApiClientFactorySettings(r365Configuration);
                    var connectorNotifications = await _notificationPullManager.GetAllPendingConnectorTypeNotifications(
                        apiClientFactorySettings, authenticationHelperSettings,
                        connectorType, cancellationToken);
                    notifications.AddRange(connectorNotifications);
                }
                catch (Exception ex)
                {
                    _observabilityScope.Invoke(GetDimensions(), () =>
                    {
                        _telemetryTracker.TrackException(ex);
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
            await _observabilityScope.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration(notification.ConnectorConfig.ConnectorTypeConfigurationId);
                    await _notificationPullManager.AcknowledgeNotification(
                        GetApiClientFactorySettings(r365Configuration),
                        GetAuthenticationHelperSettings(r365Configuration, notification.ConnectorConfig.TenantDomainName),
                        notification.ToAcknowledge(result, message),
                        cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
        }
    }
}
