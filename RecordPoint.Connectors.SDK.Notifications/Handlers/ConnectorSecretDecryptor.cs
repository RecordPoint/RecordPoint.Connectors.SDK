using System.Security.Cryptography;
using System.Text;
using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>Decrypts platform secrets, whether they arrive on a notification or a request.</summary>
    public interface IConnectorSecretDecryptor
    {
        /// <summary>Replaces each secret's value with its decrypted form, in place.</summary>
        /// <param name="secrets">The secrets to decrypt. Null or empty is a no-op.</param>
        /// <param name="connectorConfig">The configuration whose credentials supply the key.</param>
        void DecryptInPlace(IList<ConnectorSecret>? secrets, ConnectorConfigModel connectorConfig);
    }

    /// <inheritdoc/>
    public class ConnectorSecretDecryptor : IConnectorSecretDecryptor
    {
        private readonly IR365ConfigurationClient _configurationClient;

        /// <param name="configurationClient">Supplies the client id and secret the key derives from.</param>
        public ConnectorSecretDecryptor(IR365ConfigurationClient configurationClient)
        {
            _configurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
        }

        /// <inheritdoc/>
        public void DecryptInPlace(IList<ConnectorSecret>? secrets, ConnectorConfigModel connectorConfig)
        {
            if (secrets == null || secrets.Count == 0)
            {
                return;
            }

            ArgumentNullException.ThrowIfNull(connectorConfig);

            // Read once: every secret in a delivery shares a configuration, so it shares a key.
            var r365Options = _configurationClient.GetR365Configuration(connectorConfig.ConnectorTypeConfigurationId);
            var clientId = r365Options?.ClientId ?? throw new RequiredValueNullException(nameof(R365ConfigurationModel.ClientId));
            var clientSecret = r365Options.ClientSecret ?? throw new RequiredValueNullException(nameof(R365ConfigurationModel.ClientSecret));

            // SHA256 and MD5 only to reach the lengths AES needs, matching how the platform derives them.
            var key = SHA256.HashData(Encoding.ASCII.GetBytes(clientSecret));
            var iv = MD5.HashData(Encoding.ASCII.GetBytes(clientId));

            foreach (var secret in secrets)
            {
                secret.Value = Decrypt(secret.Value, key, iv);
            }
        }

        /// <remarks>One decryptor per secret: CBC carries state, so a reused one returns rubbish.</remarks>
        private static string Decrypt(string value, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream msDecrypt = new(Convert.FromBase64String(value));
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
