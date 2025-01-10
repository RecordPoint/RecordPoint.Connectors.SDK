using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Definition for a Content Registration Request action
    /// </summary>
    public interface IContentRegistrationRequestAction
    {
        /// <summary>
        /// Obtains a list of channels to perform content registration from the requested context
        /// </summary>
        /// <param name="connectorConfiguration">The Connector configuration</param>
        /// <param name="contentRegistrationRequest">The request context used to obtain the channels to register</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the registration operation</returns>
        Task<List<Channel>> GetChannelsFromRequestAsync(ConnectorConfigModel connectorConfiguration, ContentRegistrationRequest contentRegistrationRequest, CancellationToken cancellationToken);
    }
}