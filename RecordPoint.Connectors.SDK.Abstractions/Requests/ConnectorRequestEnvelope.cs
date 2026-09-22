using RecordPoint.Connectors.SDK.Abstractions.Content;

namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>
    /// A request delivered to a connector by the <c>ConnectorRequest</c> notification.
    /// </summary>
    /// <remarks>PascalCase to match the platform: the read is case-sensitive and fails silently.</remarks>
    public class ConnectorRequestEnvelope
    {
        /// <summary>Which kind of request this is. The SDK uses it to pick the handler.</summary>
        public string RequestType { get; set; } = string.Empty;

        /// <summary>Identifies this request, and is stored with the answer.</summary>
        public string CorrelationId { get; set; } = string.Empty;

        /// <summary>Opaque, copied onto the answer by the SDK. A handler neither reads nor sets it.</summary>
        public string ResponseScope { get; set; } = string.Empty;

        /// <summary>Data for this kind of request, such as the field values to validate.</summary>
        public object? Payload { get; set; }

        /// <summary>Decrypted by the SDK before the handler runs. For this request only.</summary>
        public IList<ConnectorSecret> Secrets { get; set; } = new List<ConnectorSecret>();
    }
}
