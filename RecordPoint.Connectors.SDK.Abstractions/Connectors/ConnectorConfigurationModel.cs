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
        /// The location that generated reports are uploaded
        /// </summary>
        public string ReportLocation { get; set; } = string.Empty;
    }
}