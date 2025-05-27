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
        private readonly IR365ConfigurationClient _configurationClient;

        /// <summary>
        /// Handler for a connector secret notification
        /// </summary>
        /// <param name="connectorManager"></param>
        /// <param name="connectorSecretAction"></param>
        /// <param name="configurationClient"></param>
        public ConnectorSecretHandler(IConnectorConfigurationManager connectorManager, IConnectorSecretAction connectorSecretAction, IR365ConfigurationClient configurationClient)
        {
            _connectorManager = connectorManager;
            _connectorSecretAction = connectorSecretAction;
            _configurationClient = configurationClient;
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
            foreach (var secret in connectorSecrets)
            {
                secret.Value = DecryptSecret(secret, notification.ConnectorConfig);
            }

            var connectorConfiguration = await _connectorManager.GetConnectorAsync(notification.ConnectorId, cancellationToken);

            await _connectorSecretAction.SaveSecretsAsync(connectorConfiguration, connectorSecrets);

            return NotificationOutcome.OK();
        }

        /// <summary>
        /// Decrypt secret from notification using ClientSecret as Key and ClientId as IV for AES algorithm.
        /// Expects secret to be a Base64String for easy translation to byte array.
        /// </summary>
        /// <param name="secret">Base64string to decrypt.</param>
        /// <param name="connectorConfig">Connector Config the notification belongs to.</param>
        /// <returns>decrypted secret value.</returns>
        /// <exception cref="RequiredValueNullException"></exception>
        private string DecryptSecret(ConnectorSecret secret, ConnectorConfigModel connectorConfig)
        {
            // Fetch values for decryption
            var r365Options = _configurationClient.GetR365Configuration(connectorConfig.ConnectorTypeConfigurationId);
            var clientId = r365Options?.ClientId ?? throw new RequiredValueNullException(nameof(r365Options.ClientSecret));
            var clientSecret = r365Options.ClientSecret ?? throw new RequiredValueNullException(nameof(r365Options.ClientId));

            string secretValue;
            using (Aes aes = Aes.Create())
            {
                // Setup Decryptor
                var keyBytes = Encoding.ASCII.GetBytes(clientSecret);
                var ivBytes = Encoding.ASCII.GetBytes(clientId);
                var key = SHA256.Create().ComputeHash(keyBytes); // Ensures key is 32 bytes
                var iv = MD5.Create().ComputeHash(ivBytes); // Ensures IV is 16 bytes
                aes.Key = key;
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var secretBytes = Convert.FromBase64String(secret.Value);

                // Create the streams used for decryption.
                using MemoryStream msDecrypt = new(secretBytes);
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);

                // Read bytes to decrypt to string
                secretValue = srDecrypt.ReadToEnd();
            }
            return secretValue;
        }
    }
}
