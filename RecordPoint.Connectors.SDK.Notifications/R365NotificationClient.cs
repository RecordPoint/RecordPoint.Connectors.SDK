using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
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
        private readonly IScopeManager _scopeManager;

        public R365NotificationClient(
            IR365ConfigurationClient r365ConfigurationClient,
            IScopeManager scopeManager)
        {
            _r365ConfigurationClient = r365ConfigurationClient;
            _scopeManager = scopeManager;
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

        public Task<IList<ConnectorNotificationModel>> GetAllPendingNotifications(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

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
