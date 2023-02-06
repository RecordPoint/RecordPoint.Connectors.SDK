using RecordPoint.Connectors.SDK.Client.Models;
using System;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Connector config extensions as part of the framework.
    /// </summary>
    public static class ConnectorConfigExtensions
    {
        public const string CONSENT_AUTHORIZED_ON_PROPERTY = "ConsentAuthorizedOn";
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

        public static void SetConsentAuthorizedOn(this ConnectorConfigModel connectorConfig, DateTimeOffset value)
        {
            connectorConfig.SetProperty(CONSENT_AUTHORIZED_ON_PROPERTY, value.ToString("o"), "DateTimeOffset");
        }
    }
}
