namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Provides direct (non-EF Core) access to channel data for optimised reads.
    /// When enabled, bypasses EF Core query translation to use native point reads.
    /// </summary>
    public interface IDirectChannelAccess
    {
        /// <summary>
        /// Whether direct channel access is enabled.
        /// When false, callers should fall back to the standard EF Core path.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Reads a single channel by connector ID and external ID using a direct point read.
        /// </summary>
        /// <param name="connectorId">The connector ID (partition key)</param>
        /// <param name="externalId">The external ID (document ID)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The channel model if found; null otherwise</returns>
        Task<ChannelModel?> ReadChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Streams lightweight channel classification data for a connector.
        /// </summary>
        /// <param name="connectorId">The connector ID (partition key)</param>
        /// <param name="pageSize">Preferred page size for providers that support paged reads</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Async stream of lightweight channel projections</returns>
        IAsyncEnumerable<ChannelClassificationModel> ReadChannelClassificationsAsync(string connectorId, int pageSize, CancellationToken cancellationToken);
    }
}
