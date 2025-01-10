using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Definition for a Connector Secret action
    /// </summary>
    public interface IConnectorSecretAction
    {
        /// <summary>
        /// Save the secret for the connector configuration instance
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="secrets">The secrets to be saved by the connector</param>
        Task SaveSecretsAsync(ConnectorConfigModel connectorConfiguration, IList<ConnectorSecret> secrets);
    }
}
