namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Connector Database Options
    /// </summary>
    public class ConnectorDatabaseOptions
    {
        /// <summary>
        /// Default database name
        /// </summary>
        private const string DEFAULT_DATABASE_NAME = "ConnectorConfig";

        /// <summary>
        /// Name of the database
        /// </summary>
        public string DatabaseName { get; set; } = DEFAULT_DATABASE_NAME;
    }
}