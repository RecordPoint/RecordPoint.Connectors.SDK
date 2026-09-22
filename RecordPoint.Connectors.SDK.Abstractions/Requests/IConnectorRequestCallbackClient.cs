using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>Returns a connector's answer to the platform, on its own request.</summary>
    public interface IConnectorRequestCallbackClient
    {
        /// <summary>Posts an answer for the connector configuration the request was sent to.</summary>
        /// <param name="connectorConfig">The configuration the request was sent to.</param>
        /// <param name="response">The answer produced by the handler.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <remarks>A parameter, not part of the answer, so a handler never addresses anything.</remarks>
        Task SendAsync(ConnectorConfigModel connectorConfig, ConnectorRequestResponse response, CancellationToken cancellationToken);
    }
}
