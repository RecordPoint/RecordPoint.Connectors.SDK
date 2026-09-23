using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using System;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Connector config extensions as part of the framework.
    /// </summary>
    public static class ConnectorConfigExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string CONSENT_AUTHORIZED_ON_PROPERTY = "ConsentAuthorizedOn";
        /// <summary>
        /// Get consent authorized on.
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration.</param>
        /// <returns>A DateTimeOffset?</returns>
        public static DateTimeOffset? GetConsentAuthorizedOn(this ConnectorConfigModel connectorConfiguration)
        {
            if (DateTimeOffset.TryParse(connectorConfiguration.GetPropertyOrDefault(CONSENT_AUTHORIZED_ON_PROPERTY), out DateTimeOffset ConsentAuthorizedOn))
            {
                return ConsentAuthorizedOn;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Set consent authorized on.
        /// </summary>
        /// <param name="connectorConfig">The connector config.</param>
        /// <param name="value">The value.</param>
        public static void SetConsentAuthorizedOn(this ConnectorConfigModel connectorConfig, DateTimeOffset value)
        {
            connectorConfig.SetProperty(CONSENT_AUTHORIZED_ON_PROPERTY, value.ToString("o"), "DateTimeOffset");
        }

        /// <summary>
        /// The connector config property storing whether binaries are submitted to the platform.
        /// </summary>
        public const string BINARY_SUBMISSION_ENABLED_PROPERTY = "BinarySubmissionEnabled";

        /// <summary>
        /// The stored binary submission setting, or null when the connector has no such property.
        /// Only connectors whose manifest carries the switcher have this property; the null case
        /// signals "no toggle, fall back to the legacy content-protection toggle".
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration.</param>
        /// <returns>A bool?</returns>
        public static bool? GetBinarySubmissionEnabled(this ConnectorConfigModel connectorConfiguration)
        {
            var value = connectorConfiguration.GetPropertyOrDefault(BINARY_SUBMISSION_ENABLED_PROPERTY);
            if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out var enabled))
            {
                return enabled;
            }

            return null;
        }

        /// <summary>
        /// Set whether binaries are submitted for this connector, in config.
        /// </summary>
        /// <param name="connectorConfig">The connector config.</param>
        /// <param name="value">The value.</param>
        public static void SetBinarySubmissionEnabled(this ConnectorConfigModel connectorConfig, bool value)
        {
            connectorConfig.SetProperty(BINARY_SUBMISSION_ENABLED_PROPERTY, value.ToString(), nameof(Boolean));
        }

        /// <summary>
        /// Convert a connector data model back into a connector config
        /// </summary>
        /// <param name="connectorData">Connector data to convert</param>
        /// <returns>Connector config</returns>
        public static ConnectorConfigModel ConvertToConnectorConfig(this ConnectorConfigurationModel connectorData)
        {
            return JsonSerializer.Deserialize<ConnectorConfigModel>(connectorData.Data);
        }
    }
}
