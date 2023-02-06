namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    public class CosmosDbConnectorDatabaseOptions
    {
        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "CosmosDbConnectorDatabase";

        /// <summary>
        /// Default database name
        /// </summary>
        public const string DEFAULT_DATABASE_NAME = "connector-db";

        /// <summary>
        /// Name of the database
        /// </summary>
        public string DatabaseName { get; set; } = DEFAULT_DATABASE_NAME;

        /// <summary>
        /// Connection string for the database to be used
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// The endpoint to the Cosmos Resource
        /// </summary>
        public string CosmosDbAccountName { get; set; } = string.Empty;
    }
}
