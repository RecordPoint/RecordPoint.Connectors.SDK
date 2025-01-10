using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines content synchronisation action, which is responsible for synchronising content within the Content Source from the specified start time, or point where the last scan completed via the provided cursor
    /// </summary>
    public interface IContentSynchronisationAction
    {
        /// <summary>
        /// Begin a new content synchronisation operation that will generate a stream of content starting from a specific point in time
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="startDate">Start date time of the synncronisation operation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the content synchronisation</returns>
        Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, DateTimeOffset startDate, CancellationToken cancellationToken);

        /// <summary>
        /// Continue a content synchronisation operation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="cursor">Scan cursor provided by the prior sync operation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the content synchronisation</returns>
        Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken);

        /// <summary>
        /// Stops the content synchronisation operation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="channel">The channel to perform content registration on</param>
        /// <param name="cursor">Scan cursor provided by the prior sync operation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <remarks>
        /// Stops a current content synchronisation operation. Typically because a connector has been disabled, kill switch has been hit etc.
        /// Some content modules may use this to stop expensive services that might be running in order to support the synchronisation operation.
        /// Content modules should restart processing when asked to continue
        /// </remarks>
        Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken);
    }
}