using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Definition for a Content Registration action
    /// </summary>
    public interface IContentRegistrationAction
    {
        /// <summary>
        /// Begin a new content registration operation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="context">Custom Context for the Registration action</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the registration operation</returns>
        Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken);

        /// <summary>
        /// Continue a content registration
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="cursor">Scan cursor provided by the prior sync operation</param>
        /// <param name="context">Custom Context for the Registration action</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the registration operation</returns>
        Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken);

        /// summary>
        /// Stops the content synchronisation operation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <remarks>
        /// Stops a current content synchronisation operation. Typically because a connector has been disabled, kill switch has been hit etc.
        /// Some content modules may use this to stop expensive services that might be running in order to support the synchronisation operation.
        /// Content modules should restart processing when asked to continue
        /// </remarks>
        Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken);
    }
}