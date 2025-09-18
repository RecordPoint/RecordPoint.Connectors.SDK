using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a Content Manager callback which is executed after the content manager operation is invoked
    /// </summary>
    /// <remarks>
    /// May be used to add custom Work Operations.
    /// e.g. a replacement service for Content Sync that runs per tenant instead of per channel
    /// </remarks>
    public interface IContentManagerCallbackAction
    {
        /// <summary>
        /// Executes when new connector configurations are discovered during the content manager operation
        /// </summary>
        /// <param name="connectorConfigurations">The new Connector Configurations that have been found</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ExecuteAsync(List<ConnectorConfigModel> connectorConfigurations, CancellationToken cancellationToken);
    }
}