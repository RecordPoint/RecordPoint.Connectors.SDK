using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Requests;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Reads a ConnectorRequest notification, dispatches it, and returns the answer to the platform.
    /// </summary>
    /// <remarks>One notification type for every request type, so a new one needs no transport change.</remarks>
    public class ConnectorRequestHandler : NotificationHandler
    {
        /// <summary>
        /// Connector Request Notification Type
        /// </summary>
        public const string CONNECTOR_REQUEST_NOTIFICATION_TYPE = "ConnectorRequest";

        private readonly Dictionary<string, IConnectorRequestHandler> _handlers;
        private readonly IConnectorRequestCallbackClient _callbackClient;
        private readonly IConnectorSecretDecryptor _secretDecryptor;
        private readonly ILogger<ConnectorRequestHandler> _logger;

        /// <summary>
        /// Handles the connector request notification.
        /// </summary>
        /// <param name="handlers">The request handlers this connector registered, one per request type.</param>
        /// <param name="callbackClient">Returns the answer to the platform.</param>
        /// <param name="secretDecryptor">Decrypts the request's secrets before a handler sees them.</param>
        /// <param name="logger">Logger.</param>
        public ConnectorRequestHandler(
            IEnumerable<IConnectorRequestHandler> handlers,
            IConnectorRequestCallbackClient callbackClient,
            IConnectorSecretDecryptor secretDecryptor,
            ILogger<ConnectorRequestHandler> logger)
        {
            // Last registration wins, so a connector can replace a handler the SDK ships.
            var byRequestType = new Dictionary<string, IConnectorRequestHandler>(StringComparer.OrdinalIgnoreCase);
            foreach (var handler in handlers ?? Enumerable.Empty<IConnectorRequestHandler>())
            {
                byRequestType[handler.RequestType] = handler;
            }

            _handlers = byRequestType;
            _callbackClient = callbackClient ?? throw new ArgumentNullException(nameof(callbackClient));
            _secretDecryptor = secretDecryptor;
            _logger = logger;
        }

        /// <summary>
        /// Connector Request Notification Type
        /// </summary>
        public override string NotificationType => CONNECTOR_REQUEST_NOTIFICATION_TYPE;

        /// <summary>Answers the request carried on the notification.</summary>
        /// <param name="notification">The notification, carrying the request envelope as its context.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <remarks>OK whenever an answer was sent, even a failed one. Neither causes redelivery.</remarks>
        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);

            ConnectorRequestEnvelope? request;
            try
            {
                request = notification.Context.ContextToObject<ConnectorRequestEnvelope>();
            }
            catch (Exception ex)
            {
                // No correlation id, so there is no way to answer.
                _logger?.LogError(ex, "A connector request notification carried a context that could not be read as a request.");
                return NotificationOutcome.Failed("The connector request could not be read.");
            }

            if (request == null || string.IsNullOrWhiteSpace(request.RequestType))
            {
                _logger?.LogError("A connector request notification carried no request type.");
                return NotificationOutcome.Failed("The connector request carried no request type.");
            }

            var response = TryDecryptSecrets(request, notification.ConnectorConfig)
                ? await ProduceAnswerAsync(notification.ConnectorConfig, request, cancellationToken).ConfigureAwait(false)
                : ConnectorRequestResponse.Failed(
                    request.CorrelationId, "The credentials supplied with this request could not be read.");

            // Set here, not taken from the handler. These say which request is being answered.
            response.RequestType = request.RequestType;
            response.CorrelationId = request.CorrelationId;
            response.ResponseScope = request.ResponseScope;

            try
            {
                await _callbackClient.SendAsync(notification.ConnectorConfig, response, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Nothing redelivers this, so the answer is lost. Reported as failed for telemetry.
                _logger?.LogError(ex, "The answer to connector request {RequestType} could not be returned.", request.RequestType);
                return NotificationOutcome.Failed("The answer could not be returned to the platform.");
            }

            return NotificationOutcome.OK();
        }

        /// <summary>Turns the request's secrets into values a handler can use.</summary>
        /// <returns>False when they could not be read, so the request is answered as failed.</returns>
        private bool TryDecryptSecrets(ConnectorRequestEnvelope request, ConnectorConfigModel connectorConfig)
        {
            try
            {
                _secretDecryptor?.DecryptInPlace(request.Secrets, connectorConfig);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "The secrets on connector request {RequestType} could not be decrypted.", request.RequestType);
                return false;
            }
        }

        private async Task<ConnectorRequestResponse> ProduceAnswerAsync(
            ConnectorConfigModel connectorConfiguration,
            ConnectorRequestEnvelope request,
            CancellationToken cancellationToken)
        {
            if (!_handlers.TryGetValue(request.RequestType, out var handler))
            {
                // Answered, not dropped: "not supported" beats silence.
                _logger?.LogWarning("No handler is registered for connector request type {RequestType}.", request.RequestType);
                return ConnectorRequestResponse.Failed(
                    request.CorrelationId,
                    $"This connector does not support the request type '{request.RequestType}'.");
            }

            try
            {
                var response = await handler
                    .HandleAsync(connectorConfiguration, request, cancellationToken).ConfigureAwait(false);

                return response ?? ConnectorRequestResponse.Failed(
                    request.CorrelationId,
                    $"The handler for '{request.RequestType}' returned no answer.");
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // Shutdown, not a failed request: do not record a failure against an unattempted one.
                throw;
            }
            catch (Exception ex)
            {
                // Logged in full, reported in the abstract: exception text carries credentials and URLs.
                _logger?.LogError(ex, "The handler for connector request type {RequestType} threw.", request.RequestType);
                return ConnectorRequestResponse.Failed(
                    request.CorrelationId,
                    "The connector could not answer the request. See the connector's logs for the reason.");
            }
        }
    }
}
