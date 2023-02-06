using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.R365
{

    /// <summary>
    /// Provides access to R365
    /// </summary>
    public interface IR365Client
    {

        /// <summary>
        /// Is Records 365 Configured?
        /// </summary>
        /// <remarks>
        /// Note that the client can be "ready" (All its dependant services are ready) but the configuration not yet loaded.
        /// </remarks>
        /// <returns>True if records 365 has been configured and we should be able to invoke it, otherwise false</returns>
        bool IsConfigured();

        /// <summary>
        /// Submit an Audit Event
        /// </summary>
        /// <param name="connectorConfig">Configuration for the Connector</param>
        /// <param name="auditEvent">Audit Event to submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        public Task<SubmitResult> SubmitAuditEvent(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, CancellationToken cancellationToken);

        /// <summary>
        /// Submit a record
        /// </summary>
        /// <param name="connectorConfig">Configuration for the Connector</param>
        /// <param name="record">Record to submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        public Task<SubmitResult> SubmitRecord(ConnectorConfigModel connectorConfig, Record record, CancellationToken cancellationToken);

        /// <summary>
        /// Submit an aggregation to records 365
        /// </summary>
        /// <param name="connectorConfig">Configuration for the Connector</param>
        /// <param name="aggregation">Aggregation to submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        public Task<SubmitResult> SubmitAggregation(ConnectorConfigModel connectorConfig, Aggregation aggregation, CancellationToken cancellationToken);

        /// <summary>
        /// Submit a binary to records 365
        /// </summary>
        /// <param name="connectorConfig">Configuration for the Connector</param>
        /// <param name="binaryMetaInfo">Metainfo for the binary being submitted</param>
        /// <param name="binaryStream">Stream of the binary to be submitted</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        public Task<SubmitResult> SubmitBinary(ConnectorConfigModel connectorConfig, BinaryMetaInfo binaryMetaInfo, Stream binaryStream, CancellationToken cancellationToken);

    }
}
