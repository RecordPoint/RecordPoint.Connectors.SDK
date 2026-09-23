namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>
    /// The answer a connector produces for a <see cref="ConnectorRequestEnvelope"/>.
    /// </summary>
    public class ConnectorRequestResponse
    {
        /// <summary>Copied from the request by the SDK. You do not set this.</summary>
        public string RequestType { get; set; } = string.Empty;

        /// <summary>The correlation id copied from the originating request.</summary>
        public string CorrelationId { get; set; } = string.Empty;

        /// <summary>Copied from the request by the SDK. You do not set this.</summary>
        public string ResponseScope { get; set; } = string.Empty;

        /// <summary>The overall outcome of the request.</summary>
        public RequestOutcomeType Outcome { get; set; }

        /// <summary>Human-readable messages describing the outcome.</summary>
        public IList<string> Messages { get; set; } = new List<string>();

        /// <summary>Data for this kind of answer, such as the objects found in the source.</summary>
        public object? Data { get; set; }

        /// <summary>Creates a successful answer.</summary>
        /// <param name="correlationId">The request's correlation id.</param>
        /// <param name="message">An optional message.</param>
        /// <param name="data">Optional data.</param>
        public static ConnectorRequestResponse Ok(string correlationId, string? message = null, object? data = null)
        {
            var response = new ConnectorRequestResponse
            {
                CorrelationId = correlationId,
                Outcome = RequestOutcomeType.Ok,
                Data = data
            };

            if (!string.IsNullOrEmpty(message))
            {
                response.Messages.Add(message);
            }

            return response;
        }

        /// <summary>Creates a failed answer.</summary>
        /// <param name="correlationId">The request's correlation id.</param>
        /// <param name="message">What went wrong.</param>
        public static ConnectorRequestResponse Failed(string correlationId, string message)
        {
            var response = new ConnectorRequestResponse
            {
                CorrelationId = correlationId,
                Outcome = RequestOutcomeType.Failed
            };

            if (!string.IsNullOrEmpty(message))
            {
                response.Messages.Add(message);
            }

            return response;
        }
    }
}
