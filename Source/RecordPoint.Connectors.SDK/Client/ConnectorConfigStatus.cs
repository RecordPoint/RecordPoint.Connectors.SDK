namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Defines constant values that may appear in the ConnectorConfigModel.Status field.
    /// </summary>
    public static class ConnectorConfigStatus
    {
        /// <summary>
        /// Indicates the Connector is in the Enabled status.
        /// The connector is expected to operate as normal in this status.
        /// </summary>
        public const string Enabled = nameof(Enabled);

        /// <summary>
        /// Indicates the connector is in the Disabled status.
        /// The connector is expected to do nothing in this status.
        /// </summary>
        public const string Disabled = nameof(Disabled);

        /// <summary>
        /// Indicates the connector is in the Error status.
        /// The connector is expected to do nothing in this status.
        /// </summary>
        public const string Error = nameof(Error);
    }
}
