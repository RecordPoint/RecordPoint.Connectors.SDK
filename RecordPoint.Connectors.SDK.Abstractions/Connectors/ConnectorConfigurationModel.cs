using System.ComponentModel.DataAnnotations;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Data model for connector data stored in the database
    /// </summary>
    public class ConnectorConfigurationModel
    {
        /// <summary>
        /// Connector Id as supplied from records 365. Usually a guid.
        /// </summary>
        [Key]
        public string ConnectorId { get; set; } = string.Empty;

        /// <summary>
        /// Connector Type Id as supplied from records 365. Usually a guid.
        /// </summary>
        public string ConnectorTypeId { get; set; } = string.Empty;

        /// <summary>
        /// ID of the records 365 tenant that contains the connector.
        /// </summary>
        public string TenantId { get; set; } = string.Empty;

        /// <summary>
        /// Display name of the connector
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Status of the connector
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Serialized copy of the entire model supplied by records 365
        /// </summary>
        public string Data { get; set; } = string.Empty;

        /// <summary>
        /// The Id of the Channel Discovery Work Item
        /// </summary>
        public string? ChannelDiscoveryWorkId { get; set; } = null;

        /// <summary>
        /// The time when Channel Discovery was last enqueued for this connector
        /// </summary>
        public DateTimeOffset? ChannelDiscoveryEnqueuedDate { get; set; } = null;

        /// <summary>
        /// The time when Channel Discovery was last executed for this connector
        /// </summary>
        public DateTimeOffset? ChannelDiscoveryExecutedDate { get; set; } = null;
    }
}