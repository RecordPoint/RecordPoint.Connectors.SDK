using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a binary retriever operation which is responsible for obtaining a stram to a binary within the Content Source.
    /// </summary>
    public interface IBinaryRetrievalAction
    {
        /// <summary>
        /// Gets a stream to corresponding to the binary meta info
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="binaryMetaInfo">Meta info to get stream for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Get binary outcome</returns>
        Task<BinaryRetrievalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, CancellationToken cancellationToken);
    }
}