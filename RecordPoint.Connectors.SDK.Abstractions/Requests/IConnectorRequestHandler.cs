using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>
    /// Answers one kind of connector request. The only interface a connector implements for one.
    /// </summary>
    /// <remarks>Registered with AddConnectorRequestHandler. A type with no handler answers as failed.</remarks>
    public interface IConnectorRequestHandler
    {
        /// <summary>The request type this handler answers. Matched without regard to case.</summary>
        string RequestType { get; }

        /// <summary>Produces the answer to a request.</summary>
        /// <param name="connectorConfiguration">The instance the request is asked of, as the platform sent it.</param>
        /// <param name="request">The request, including any payload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The answer. Use <see cref="ConnectorRequestResponse.Ok"/> or Failed to build it.</returns>
        /// <remarks>
        /// Report problems by returning a failed answer: a thrown one loses its message.
        ///
        /// The configuration is the one being asked about — for Authenticate it may be edited and
        /// not yet saved, so it wins over anything stored. A handler wanting the state this
        /// connector holds instead resolves its own store by the configuration's id.
        /// </remarks>
        Task<ConnectorRequestResponse> HandleAsync(
            ConnectorConfigModel connectorConfiguration,
            ConnectorRequestEnvelope request,
            CancellationToken cancellationToken);
    }
}
