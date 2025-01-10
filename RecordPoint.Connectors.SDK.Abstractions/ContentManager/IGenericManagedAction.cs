using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// Defines a generic managed action which can be executed by an Operation.
/// </summary>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public interface IGenericManagedAction<in TInput, TOutput> where TOutput : ActionResultBase
{
    /// <summary>
    /// Begin a new generic action
    /// </summary>
    /// <param name="connectorConfiguration">The connector configuration</param>
    /// <param name="item">The item to take action in respect to</param>
    /// <param name="context">Custom Context for the action</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of the action</returns>
    Task<TOutput> BeginAsync(ConnectorConfigModel connectorConfiguration, TInput item, IDictionary<string, string>? context, CancellationToken cancellationToken);

    /// <summary>
    /// Continue a generic action
    /// </summary>
    /// <param name="connectorConfiguration">The connector configuration</param>
    /// <param name="item">The item to take action in respect to</param>
    /// <param name="cursor">Cursor provided by the prior action</param>
    /// <param name="context">Custom Context for the action</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of the action</returns>
    Task<TOutput> ContinueAsync(ConnectorConfigModel connectorConfiguration, TInput item, string cursor, IDictionary<string, string>? context, CancellationToken cancellationToken);
}