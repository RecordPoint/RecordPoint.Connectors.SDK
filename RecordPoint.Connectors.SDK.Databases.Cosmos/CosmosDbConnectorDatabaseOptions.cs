namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// The cosmos db connector database options.
    /// </summary>
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

        /// <summary>
        /// Sets the serializer to use Camel Case naming policy
        /// </summary>
        /// <remarks>Default: True</remarks>
        public bool UseCamelCaseNamingPolicy { get; set; } = true;

        /// <summary>
        /// Force to use Gateway connection mode
        /// </summary>
        public bool UseGateWayConnectionMode { get; set; } = false;

        /// <summary>
        /// TLS version to use for Cosmos DB connections (e.g., "Tls12", "Tls13", "Tls12,Tls13")
        /// </summary>
        public string? TlsVersion { get; set; }
    
    }
}
