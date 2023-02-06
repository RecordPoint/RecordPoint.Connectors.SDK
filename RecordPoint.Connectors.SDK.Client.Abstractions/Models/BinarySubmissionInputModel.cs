namespace RecordPoint.Connectors.SDK.Client.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    /// <summary>
    /// This class has been added for backwards compatibility reasons
    /// </summary>
    public partial class BinarySubmissionInputModel
    {
        /// <summary>
        /// Initializes a new instance of the BinarySubmissionInputModel class.
        /// </summary>
        public BinarySubmissionInputModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BinarySubmissionInputModel class.
        /// </summary>
        /// <param name="connectorId">The ID of the connector submitting the
        /// binary</param>
        /// <param name="itemExternalId">The ExternalID of the item that the
        /// binary belongs to</param>
        /// <param name="binaryExternalId">The ExternalID of the binary</param>
        /// <param name="fileName">An optional file name to associate with the
        /// binary</param>
        /// <param name="correlationId">An optional ID used to track the binary
        /// version as it moves through the pipeline</param>
        public BinarySubmissionInputModel(string connectorId, string itemExternalId, string binaryExternalId, string fileName = default, string correlationId = default)
        {
            ConnectorId = connectorId;
            ItemExternalId = itemExternalId;
            BinaryExternalId = binaryExternalId;
            FileName = fileName;
            CorrelationId = correlationId;
        }

        /// <summary>
        /// Gets or sets the ID of the connector submitting the binary
        /// </summary>
        [JsonProperty(PropertyName = "connectorId")]
        public string ConnectorId { get; set; } 

        /// <summary>
        /// Gets or sets the ExternalID of the item that the binary belongs to
        /// </summary>
        [JsonProperty(PropertyName = "itemExternalId")]
        public string ItemExternalId { get; set; }

        /// <summary>
        /// Gets or sets the ExternalID of the binary
        /// </summary>
        [JsonProperty(PropertyName = "binaryExternalId")]
        public string BinaryExternalId { get; set; }

        /// <summary>
        /// Gets or sets an optional file name to associate with the binary
        /// </summary>
        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; } 

        /// <summary>
        /// Gets or sets an optional ID used to track the binary version as it
        /// moves through the pipeline
        /// </summary>
        [JsonProperty(PropertyName = "correlationId")]
        public string CorrelationId { get; set; } 

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ConnectorId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ConnectorId");
            }
            if (ItemExternalId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ItemExternalId");
            }
            if (BinaryExternalId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "BinaryExternalId");
            }
        }
    }
}