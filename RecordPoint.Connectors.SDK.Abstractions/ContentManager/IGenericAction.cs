using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a generic action which can be executed by an Operation.
    /// </summary>
    public interface IGenericAction<in TInput, TOutput> where TOutput : ActionResultBase
    {
        /// <summary>
        /// Executes a custom action
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="item">The item to take action in respect to</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Action outcome</returns>
        Task<TOutput> ExecuteAsync(ConnectorConfigModel connectorConfiguration, TInput item, CancellationToken cancellationToken);
    }
}